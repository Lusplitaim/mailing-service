import { Component, OnInit } from '@angular/core';
import { CronTask } from 'src/app/models/cron-task';
import { CronTaskService } from 'src/app/services/cron-task.service';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'app-task-table',
  templateUrl: './task-table.component.html',
  styleUrls: ['./task-table.component.css']
})
export class TaskTableComponent implements OnInit {
  tasks: CronTask[] = [];

  constructor(private taskService: CronTaskService,
    private modalService: NgbModal,
    private toast: ToastService) { }

  ngOnInit(): void {
    this.getUserTasks();
  }

  openConfirmationModal(id: number, modal: any) {
    this.open(modal, id);
  }

  open(content: any, taskId: number) {
		this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then(
			(result) => {
        this.deleteTask(taskId);
			},
      (reason) => {

      }
		);
	}

  getUserTasks() {
    this.taskService.getUserTasks().subscribe((tasks: CronTask[]) => {
      this.tasks = tasks;
    });
  }

  deleteTask(id: number) {
    this.taskService.deleteTask(id).subscribe((isDeleted: boolean) => {
      if (isDeleted) {
        this.tasks = this.tasks.filter((task) => task.id !== id);
      } else {
        this.toast.showError("Could not delete task");
      }
    });
  }

}
