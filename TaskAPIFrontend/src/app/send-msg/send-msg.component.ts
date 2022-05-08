import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../customer.service';
import { MsgService } from './msg.service';
import { FormsModule, NgForm } from '@angular/forms'
import { Customer } from '../customer.model';
import {forkJoin} from "rxjs";
import {tap} from "rxjs/operators";


@Component({
  selector: 'app-send-msg',
  templateUrl: './send-msg.component.html',
  styleUrls: ['./send-msg.component.css']

})
export class SendMsgComponent implements OnInit {
varID : number=0;
selectedIDs: number[] = [];

  constructor( public service:CustomerService , public service2:MsgService) { }

  ngOnInit(): void {
    this.service2.getAllMsgs();
    this.service.getAllCustomers();
    this.service2.message={
      CustId : this.varID,
      MsgSubject:" ",
      MsgBody : " ",
      status : " "

    }
  }

  submit()
  {
    this.service2.postMsg(this.varID).subscribe(res=>{
        this.service2.getAllMsgs()
    },
    err=>{
      console.log(err)
    })
  }


  select(customerId: any)
  {

    console.log(customerId);
    this.varID=customerId;
    alert("The message sent successfully !!!")
  }

}



