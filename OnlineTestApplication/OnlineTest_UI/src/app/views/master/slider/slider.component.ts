import { Component, OnInit,ViewChild } from '@angular/core';
import { Slider } from '../../../models/master';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { HelperService } from '../../../services/helper.service';
import { APIUrl } from "../../../shared/API-end-points";
import { HostName } from '../../../shared/app-setting';
@Component({
  selector: 'app-slider',
  templateUrl: './slider.component.html',
  styleUrls: ['./slider.component.scss']
})
export class SliderComponent implements OnInit {
  baseURL:any;
  IsEmpty: boolean = false;
  btnAddNew: boolean = true;
  showAddDiv: any = false;
  slider: Slider;
  sliderModel: any = {};
  Title: any;
  dataSource: any = [];
  displayedColumns = ['SliderNo','Tittle', 'SliderImage','Visible', 'Edit', 'Delete'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  PaginationConfig: any;
  fileToUpload: File = null;
  imgURL:any="assets/img/avatars/default-avatar.png";
  constructor(private helperSvc: HelperService) { }

  ngOnInit() {
    debugger;
     this.getSlider();
    this.PaginationConfig = this.helperSvc.PaginationConfig;
    this.baseURL=HostName.API_StartPoint;
  }
  handleFileInput(files: FileList) {
    debugger;
    if (files.length === 0)
    return;

  var mimeType = files[0].type;
  if (mimeType.match(/image\/*/) == null) {
    alert( "Only images are supported.");
    return;
  }
    this.fileToUpload = files.item(0);
    var reader = new FileReader();
    reader.readAsDataURL(this.fileToUpload);
    reader.onload = (_event) => { 
       this.imgURL = reader.result; 
    }
    
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
    !this.dataSource.filteredData.length ? this.IsEmpty = true : this.IsEmpty = false;
  }
  changeShowStatus() {
    debugger;
    this.sliderModel = {};
   this.imgURL="assets/img/avatars/default-avatar.png";
    // this.helperSvc.getService_WithoutSpinner(APIUrl.GetMasterData)
    //   .subscribe(data => {
    //     if (data.Message === 'Success') {
    //       this.subject = data.Object.Subject;
    //     }
    //   }, error => {
    //     alert('error');
    //   })
    this.showAddDiv = !this.showAddDiv;
    this.Title = "Add Slider";
  }

  getSlider() {
    debugger;
    this.helperSvc.getService(APIUrl.GETSlider+ "?SliderID=0")
      .subscribe(data => {
        if (data.Message === 'Success') {
          this.dataSource = new MatTableDataSource(data.Object as Slider[]);
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
           this.slider = data.Object;
          !this.dataSource.filteredData.length ? this.IsEmpty = true : this.IsEmpty = false;
        }
      }, error => {
        this.helperSvc.errorHandler("Error : " + error);
      })
  }
  addSlider() {
    debugger;
    if(this.fileToUpload !=null)
    {
    this.helperSvc.postFile(APIUrl.UploadSliderImage,this.fileToUpload)
      .subscribe(data => {
        this.sliderModel.SliderImage = data;
        this.helperSvc.postService(APIUrl.AddUpdateSlider, this.sliderModel)
          .subscribe(data => {
            if (data === 'Success') {
              this.sliderModel = {};
              //this.exampleform.reset();
              this.showAddDiv = !this.showAddDiv;
              this.getSlider();
              this.helperSvc.notifySuccess('Record Saved Successfully.');
              this.btnAddNew = true;
            }
          }, error => {
            this.helperSvc.errorHandler("Error : " + error);
            console.log(error);
          });
      }, error => {
        this.helperSvc.errorHandler("Error : " + error);
        console.log(error);
      });
    }
    else{
      this.helperSvc.postService(APIUrl.AddUpdateSlider, this.sliderModel)
          .subscribe(data => {
            if (data === 'Success') {
              this.sliderModel = {};
              this.fileToUpload = null;
              //this.exampleform.reset();
              this.showAddDiv = !this.showAddDiv;
              this.getSlider();
              this.helperSvc.notifySuccess('Record Saved Successfully.');
              this.btnAddNew = true;
            }
          }, error => {
            this.helperSvc.errorHandler("Error : " + error);
            console.log(error);
          });
        }
  }
  deleteSlider(sliderModel:Slider) {
    //this.onlineTest = model;
    debugger;
    if (confirm("Are you sure to delete " + sliderModel.Tittle)) {
      this.helperSvc.postService(APIUrl.DeleteSlider,sliderModel)
        .subscribe(data => {
          if (data === 'Success') {
            this.getSlider();
            //this.showAddDiv = false;
            // alert('Record Deleted Successfully.');
            this.helperSvc.notifySuccess('Record Deleted Successfully.');
            //this.isTestTypeReadonly = true;
          }
        }, error => {
          this.helperSvc.errorHandler("Error : " + error);
          console.log(error);
        });
    }
  }
  getSliderById(SliderID) {
    debugger;
    this.btnAddNew = false;
    this.showAddDiv = true;
    this.Title = "Edit Slider";
    this.helperSvc.getService(APIUrl.GetSlider+ "?SliderID=" + SliderID)
      .subscribe(data => {
        if (data.Message === 'Success') {
          this.sliderModel = data.Object[0];
          
          this.sliderModel.SliderImage != '' ? this.imgURL = HostName.API_StartPoint + this.sliderModel.SliderImage : this.imgURL = 'assets/img/avatars/default-avatar.png';
        }
      }, error => {
        this.helperSvc.errorHandler(error);
      });
  }
  DownloadFile(filePath:string):void
  {
    debugger;
    this.helperSvc.getFile(APIUrl.DownloadFile+'?filePath='+filePath)
    .subscribe(fileData => 
      {
      let b:any = new Blob([fileData], { type: 'application/pdf' });
      var url= window.URL.createObjectURL(b);
        window.open(url);
      }
    );
  }
 

}
