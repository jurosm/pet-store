import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.css']
})
export class ErrorComponent implements OnInit {

  imgPath = 'assets/images/500errorpage.png';

  constructor() { }

  ngOnInit() {
  }

}
