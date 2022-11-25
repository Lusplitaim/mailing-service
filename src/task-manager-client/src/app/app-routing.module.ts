import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserStatComponent } from './admin/user-stat/user-stat.component';
import { UsersListComponent } from './admin/users-list/users-list.component';
import { AdminGuard } from './guards/admin.guard';
import { AuthGuard } from './guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { SignInComponent } from './registration/sign-in/sign-in.component';
import { SignUpComponent } from './registration/sign-up/sign-up.component';
import { TaskCreationComponent } from './tasks/task-creation/task-creation.component';
import { TaskTableComponent } from './tasks/task-table/task-table.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'users', component: UsersListComponent, canActivate: [AdminGuard]},
      {path: 'users/stat/:userId', component: UserStatComponent, canActivate: [AdminGuard]},
      {path: 'task/create', component: TaskCreationComponent},
      {path: 'tasks', component: TaskTableComponent},
    ]
  },
  {path: 'signup', component: SignUpComponent},
  {path: 'signin', component: SignInComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
