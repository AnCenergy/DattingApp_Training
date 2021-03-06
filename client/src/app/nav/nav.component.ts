import { Component, OnInit } from '@angular/core';
import { ChildActivationStart } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from '../_models/user';

import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {}
  //loggedIn: boolean = false;
  // loggedIn: boolean = false;
  

  // currentUser$!: Observable<User>;
  

  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
    // this.getCurrentUser();

  //  this.currentUser$ = this.accountService.currentUser$

  
  }

  login() {
    this.accountService.login(this.model).subscribe(response => { 
      
        console.log(response);
        // this.loggedIn = true;
      
    }, error => {console.log(error); 
   
      })
    console.log(this.accountService.currentUser$);
     }

     logout() {
       this.accountService.logout();
      //  this.loggedIn = false;
  
    }

    // getCurrentUser(){
    //   this.accountService.currentUser$.subscribe(user => {
    //     this.loggedIn = !!user;
    //   }, error => {
    //       console.log(error);
    //     })
    // }
    
}
