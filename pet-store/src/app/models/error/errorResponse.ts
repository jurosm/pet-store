import {ErrorResponseUnit} from './errorResponseUnit'
export class ErrorResponse{
    errors: ErrorResponseUnit[];
    message: string;
    getErrorMessage(fieldName: string){
        return this.errors.find(x => x.field === fieldName);
    }
}
