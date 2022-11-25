import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule }   from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NavComponent } from './nav/nav.component';
import { TaskCreationComponent } from './tasks/task-creation/task-creation.component';
import { SignUpComponent } from './registration/sign-up/sign-up.component';
import { SignInComponent } from './registration/sign-in/sign-in.component';
import { ToastComponent } from './toast/toast.component';
import { HomeComponent } from './home/home.component';
import { httpInterceptorProviders } from './interceptors';
import { TaskTableComponent } from './tasks/task-table/task-table.component';
import { UrlParamsSetupComponent } from './tasks/url-params-setup/url-params-setup.component';
import { HasRoleDirective } from './directives/has-role.directive';
import { UsersListComponent } from './admin/users-list/users-list.component';
import { UserStatComponent } from './admin/user-stat/user-stat.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    TaskCreationComponent,
    SignUpComponent,
    SignInComponent,
    ToastComponent,
    HomeComponent,
    TaskTableComponent,
    UrlParamsSetupComponent,
    HasRoleDirective,
    UsersListComponent,
    UserStatComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    httpInterceptorProviders
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
