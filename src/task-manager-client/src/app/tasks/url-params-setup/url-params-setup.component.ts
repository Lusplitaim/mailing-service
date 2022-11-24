import { Component, EventEmitter, Input, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { NgbOffcanvas } from '@ng-bootstrap/ng-bootstrap';
import { UrlParam } from 'src/app/models/url-param';

@Component({
  selector: 'app-url-params-setup',
  templateUrl: './url-params-setup.component.html',
  styleUrls: ['./url-params-setup.component.css']
})
export class UrlParamsSetupComponent implements OnInit {
  @ViewChild(TemplateRef) content!: TemplateRef<any>;
  @Input() urlParams: UrlParam[] | undefined = undefined;
  @Output() urlParamsEvent = new EventEmitter<string>();

  paramsForm!: FormGroup;

  constructor(private offcanvasService: NgbOffcanvas,
    private formBuilder: FormBuilder) { 
      this.initForm();
  }

  initForm() {
    this.paramsForm = this.formBuilder.group({
      params: this.formBuilder.array([])
    });
  }

  get params() {
    return this.paramsForm.controls["params"] as FormArray;
  }

  clearParams() {
    (this.paramsForm.controls["params"] as FormArray).clear();
  }

  ngOnInit(): void {
  }

  open() {
    this.clearParams();
    if (this.urlParams) {
      for (let urlParam of this.urlParams) {
        this.addParam(urlParam);
      }
    }

    this.offcanvasService.open(this.content, { ariaLabelledBy: 'offcanvas-basic-title' }).result.then(
			(result) => {
				if (result === "Save") {
          this.emitQueryString();
        }
			},
			(reason) => {

			},
		);
  }

  emitQueryString() {
    this.urlParamsEvent.emit(this.createQueryString());
  }

  createQueryString(): string {
    let filledParams: string[] = [];
    const urlParams = this.paramsForm.value['params'] as { name: string, value: string }[];
    for (const urlParam of urlParams) {
      if (urlParam.value) filledParams.push(`${urlParam.name}=${urlParam.value}`);
    }
    return filledParams.join('&');
  }

  addParam(param: UrlParam) {
    const paramForm = this.formBuilder.group({
        name: [param.name],
        value: [null]
    });
  
    this.params.push(paramForm);
  }

  getParamForms(): FormGroup[] {
    return this.params.controls as FormGroup[];
  }
  

}
