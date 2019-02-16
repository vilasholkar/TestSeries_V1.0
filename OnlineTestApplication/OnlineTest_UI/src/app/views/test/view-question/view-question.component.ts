import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Quiz } from '../../../views/test/quiz/models'
import { ViewQuestionService } from '../../../services/admin/view-question.service';
import { ImageDialogComponent } from '../../master/image-dialog/image-dialog.component';
import { MatDialog } from '@angular/material';
import { HelperService } from '../../../services/helper.service'
import { APIUrl } from "../../../shared/API-end-points";
import { asTextData } from '@angular/core/src/view';
@Component({
  selector: 'app-view-question',
  templateUrl: './view-question.component.html',
  styleUrls: ['./view-question.component.scss']
})
export class ViewQuestionComponent implements OnInit {

  OnlineTestId: number;
  quizModel: Quiz;
  // quizModelData: any = {};
  constructor(
    private route: ActivatedRoute,
    private viewQuestionService: ViewQuestionService,
    private helperSvc: HelperService,
    private dialog: MatDialog) { }

  ngOnInit() {
    this.OnlineTestId = +this.route.snapshot.paramMap.get('id');
    this.getQuestionsByTestId(this.OnlineTestId);
  }

  getQuestionsByTestId(OnlineTestId: number) {
    debugger;
    // this.viewQuestionService.GetQuestionsByTestId(OnlineTestId)
    this.helperSvc.getService(APIUrl.GetQuestionsByTestId + "?OnlineTestId=" + OnlineTestId)
      .subscribe(data => {
        if (data.Message === 'Success')
          this.quizModel = data.Object;
        else
          alert("No Questions Uploaded.");
      }, error => {

        console.log(error);
        this.helperSvc.errorHandler(error);
      });
  }

  openDialog(image_url: string): void {
   // this.helperSvc.hide_Sidebar();
    const dialogRef = this.dialog.open(ImageDialogComponent, {
      // height: '400px',
      // width: '600px',
      data: { name: 'vaibhav', image_url: image_url }
      
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      // this.animal = result;
     // this.helperSvc.show_Sidebar();
    });
  }
}
