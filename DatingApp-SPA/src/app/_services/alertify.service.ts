import {Injectable} from '@angular/core';
//We declare a variable here to keep eslint from giving errors about using an undeclared
// variable in other files (because now justify is available globally.)
declare let alertify: any;

@Injectable({
    providedIn: 'root'
})
export class AlertifyService {
    constructor() {}

    confirm(message: string, okCallback: () => any) {
        alertify.confirm(message, function(e) {
            if (e) {
                okCallback();
            } else {}
        });
    }

    success(message: string) {
        alertify.success(message);
    }
    error(message: string) {
        alertify.error(message);
    }
    warning(message: string) {
        alertify.warning(message);
    }
    message(message: string) {
        alertify.message(message);
    }
}