import { ErrorHandler, Injectable, Injector, NgZone } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Injectable()
export class AppErrorHandler implements ErrorHandler {
  private toastr: ToastrService;
  private router: Router;

  errorMessage: string = "test";
  errMsg: boolean = false;
  codeErrors: Array<any> = [
    { code: 400, err: "Invalid request." },
    { code: 401, err: "You do not have enough permission to view this page." },
    { code: 403, err: "You do not have enough permission to view this page." },
    { code: 404, err: "This page not exist." },
    { code: 500, err: "An unexpected error happend server." }
  ];

  constructor(
    private injector: Injector,
    private ngZone: NgZone,
  ) { }

  handleError(error: any) {
    this.toastr = this.injector.get(ToastrService);
    this.router = this.injector.get(Router);

    this.setErrorMessage(error.status);
    this.ngZone.run(() => {
      this.toastr.error(this.errorMessage, "Error", { timeOut: 5000 });
      this.errorMessage = null;
    });

    console.error(error);
    this.router.navigate(['/panel']);
    
    //throw error;
  }

  setErrorMessage(code: number) {
    this.codeErrors.forEach(element => {
      if (code == element['code']) {
        this.errorMessage = element['err'];
        this.errMsg = true;
      }
    });
    if (!this.errMsg)
      this.errorMessage = "An unexpected error happend.";

    this.errMsg = false;
  }
}