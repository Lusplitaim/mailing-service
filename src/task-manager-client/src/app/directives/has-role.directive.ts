import { Directive, Input, TemplateRef, ViewContainerRef } from '@angular/core';
import { take } from 'rxjs';
import { User } from '../models/user';
import { RegistrationService } from '../services/registration.service';

@Directive({
  selector: '[appHasRole]'
})
export class HasRoleDirective {
  @Input() appHasRole!: string;
  user!: User | null;

  constructor(private viewContainerRef: ViewContainerRef,
    private templateRef: TemplateRef<any>,
    private regService: RegistrationService) {
      this.regService.currentUser$.pipe<User | null>(take(1)).subscribe(user => {
        this.user = user;
      });
  }
  
  ngOnInit(): void {
    if (!this.user?.role || this.user == null) {
      this.viewContainerRef.clear();
      return;
    }

    if (this.user?.role === this.appHasRole) {
      this.viewContainerRef.createEmbeddedView(this.templateRef);
    } else {
      this.viewContainerRef.clear();
    }
  }

}
