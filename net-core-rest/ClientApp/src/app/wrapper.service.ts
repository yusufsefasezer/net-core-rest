import { Injectable } from '@angular/core';
import { ContactService } from './contact.service';
import { GlobalService } from './global.service';
import { Router } from '@angular/router';
import { Contact } from './contact';

@Injectable({
  providedIn: 'root'
})
export class WrapperService {

  constructor(
    public contactService: ContactService,
    public globalService: GlobalService,
    private router: Router
  ) {
    this.getContacts();
  }

  getContact(id: number): Contact {
    let foundContact = this.globalService.contacts.find((contact) => {
      return contact.id === id;
    });
    return foundContact as Contact;
  }

  setContact(id: number) {
    this.globalService.currentContact = this.getContact(id);
  }

  setCopyContact(id: number) {
    this.globalService.currentContact = Object.create(this.getContact(id));
  }

  isUndefined(id: number): boolean {
    return this.getContact(id) === undefined;
  }

  checkContact(id: number): void {
    if (this.isUndefined(id)) {
      this.router.navigate(['/']);
      this.globalService.resetStatus();
    }
  }

  async getContacts() {
    this.contactService.getContacts().subscribe(contacts => this.globalService.contacts = contacts);
  }

  addContact() {
    this.contactService.addContact(this.globalService.currentContact).subscribe(result => {
      this.getContacts();
    });
  }

  updateContact() {
    const foundContact = this.getContact(this.globalService.currentContact.id);
    Object.assign(foundContact, this.globalService.currentContact);
    this.contactService.updateContact(foundContact).subscribe(result => {
      this.getContacts();
    });
  }

  deleteContact() {
    this.contactService.deleteContactById(this.globalService.currentContact.id).subscribe(result => {
      this.getContacts();
      if (this.globalService.contacts.length > 0) {
        const id = this.globalService.contacts[0].id;
        this.router.navigate(['/contact', id]);
      } else {
        this.globalService.resetStatus();
        this.router.navigate(['/']);
      }
    });
  }

  deleteContacts() {
    this.globalService.contacts
      .filter((contact) => {
        return contact.checked;
      })
      .forEach((value) => {
        this.contactService.deleteContactById(value.id).subscribe(result => {
          this.getContacts();
        });
      });
    this.globalService.resetStatus();
    this.router.navigate(['/']);
  }

  showImage(event: Event) {
    const targetElement: HTMLInputElement = event.target as HTMLInputElement;
    const selectedFiles: FileList = targetElement.files as FileList;

    if (selectedFiles.length !== 1) {
      return false;
    }

    const selectedFile = selectedFiles[0];
    if (!selectedFile.type.match('image.*')) {
      return false;
    }

    const fileReader: FileReader = new FileReader();

    fileReader.addEventListener('load', (readerEvent: any) => {

      const newImage = new Image();
      const canvas: HTMLCanvasElement = document.createElement('canvas');
      const ctx: CanvasRenderingContext2D = canvas.getContext('2d') as CanvasRenderingContext2D;
      const MAX = this.globalService.MAX;

      newImage.addEventListener('load', () => {

        const ratio = MAX / newImage.height;
        newImage.width *= ratio;
        newImage.height = MAX;

        ctx.clearRect(0, 0, newImage.width, newImage.height);
        canvas.width = newImage.width;
        canvas.height = newImage.height;
        ctx.drawImage(newImage, 0, 0, newImage.width, newImage.height);
        this.globalService.currentContact.photo = this.globalService.RESIZE_IMAGE ? canvas.toDataURL() : newImage.src;

      });

      newImage.src = readerEvent.target.result;
    });

    fileReader.readAsDataURL(selectedFile);
    return true;
  }

}

interface FileReaderEventTarget extends EventTarget {
  result: string;
}

interface FileReaderEvent extends Event {
  target: FileReaderEventTarget;
}
