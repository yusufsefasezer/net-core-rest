import { Component, OnInit } from '@angular/core';
import { WrapperService } from '../wrapper.service';
import { ActivatedRoute } from '@angular/router';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent {

  constructor(
    public wrapperService: WrapperService,
    public sanitizer: DomSanitizer,
    private route: ActivatedRoute
  ) {
    this.wrapperService.globalService.createContactForm();
    this.wrapperService.globalService.showEdit();
    this.wrapperService.checkContact(this.id);
    this.wrapperService.setCopyContact(this.id);
  }

  get id(): number {
    let id: string = this.route.snapshot.params['id'];
    return parseInt(id, 10);
  }

  get f() {
    return this.wrapperService.globalService.contactForm.controls;
  }

}
