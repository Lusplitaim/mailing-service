import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TaskCreationComponent } from './tasks/task-creation/task-creation.component';

const routes: Routes = [
  {path: 'task/create', component: TaskCreationComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
