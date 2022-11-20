import { Injectable } from '@angular/core';

export interface ToastInfo {
  header: string;
  body: string;
  delay?: number;
  classname?: string;
}

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  toasts: ToastInfo[] = [];

  show(header: string, body: string) {
    this.toasts.push({ header, body, delay: 4000 });
  }

  showError(error: string) {
    this.toasts.push({ header: "Error", body: error, delay: 4000, classname: 'bg-danger text-light' });
	}

  showSuccess(message: string) {
    this.toasts.push({ header: "Success", body: message, delay: 4000, classname: 'bg-success text-light' });
	}

  remove(toast: ToastInfo) {
    this.toasts = this.toasts.filter(t => t != toast);
  }

  constructor() { }


}
