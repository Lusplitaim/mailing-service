import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignUpComponent } from './registration/sign-up/sign-up.component';
import { TaskCreationComponent } from './tasks/task-creation/task-creation.component';

const routes: Routes = [
  {path: 'task/create', component: TaskCreationComponent},
  {path: 'signup', component: SignUpComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
