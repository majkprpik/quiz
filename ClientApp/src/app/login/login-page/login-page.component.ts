import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {

  public memberList=['Marko', 'Bruno','Vedran','Marko2']

  constructor() { }

  ngOnInit(): void {
  }

}
