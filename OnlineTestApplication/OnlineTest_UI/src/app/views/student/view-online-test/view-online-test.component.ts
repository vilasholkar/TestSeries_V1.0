import { Component, OnInit } from '@angular/core';
import { StudentOnlineTestService } from '../../../services/student/student-online-test.service';
import {StudentOnlineTest} from '../../../models/student';

@Component({
  selector: 'app-view-online-test',
  templateUrl: './view-online-test.component.html',
  styleUrls: ['./view-online-test.component.scss']
})
export class ViewOnlineTestComponent implements OnInit {
  StudentID: number;
  studentOnlineTest:StudentOnlineTest;
  constructor(private studentOnlineTestService: StudentOnlineTestService) { }

  ngOnInit() {
    this.getOnlineTestByStudentID(5110);
  }


  getOnlineTestByStudentID(StudentID){
    debugger;
    this.studentOnlineTestService.getOnlineTestByStudentID(StudentID)
    .subscribe(data => {
      if (data.Message === 'Success') {
        this.studentOnlineTest = data.Object;
       }
     }, error => {
       alert('error');
       console.log(error);
     });
  }

}
