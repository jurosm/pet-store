import * as Collections from 'typescript-collections';
import { ErrorResponse } from 'src/app/models/error/errorResponse';
import { ErrorResponseUnit } from 'src/app/models/error/errorResponseUnit';

export class CollectionConverter {
  static errorArrayToDictionary(errors: ErrorResponseUnit[]): Collections.Dictionary<string, string> {
    let dict = new Collections.Dictionary<string, string>();
    if (errors !== null && errors !== undefined)
      errors.forEach(element => {
        dict[element.field] = element.message;
      });
    return dict;
  }
}

