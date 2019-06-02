import { Component, OnInit } from '@angular/core';
import * as browser from 'detect-browser';
import { Router, ActivatedRoute } from '@angular/router';

import { NgxSpinnerService } from 'ngx-spinner';
declare const MediaElementPlayer: any;

@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.scss']
})
export class PlayerComponent implements OnInit {
private videoID:string="OC09xWgTe-4";
   private player;

   // HLS
  // private song = 'http://media.crave.fm:1935/vod/mp3:TIAr0000000196Al0000000001So0000006243.mp3/playlist.m3u8';
  // private song = 'http://db3.indexcom.com/bucket/ram/00/05/64k/05.m3u8'; // Working on FireFox

  // DASH
  // private song = 'http://media.crave.fm:1935/vod/TIAr0000000196Al0000000001So0000006243.mp3/manifest.mpd';
  // private song = 'http://www.bok.net/dash/tears_of_steel/cleartext/stream.mpd';

  // RTMP
  // private song = 'rtmp://media.crave.fm:1935/vod/mp3:TIAr0000000196Al0000000001So0000006243.mp3';
  // private song = 'rtsp://media.crave.fm:1935/vod/sample.mp4'

  private song;

  // https://github.com/mediaelement/mediaelement/issues/643
  private playFlashBack = {
     mode: 'auto_plugin',
    // shows debug errors on screen
    enablePluginDebug: true,
    plugins: ['flash', 'silverlight' ],
    videoWidth: '100%',
    videoHeight: '100%',
    enableAutosize: true,
    // poster:	'../../assets/1.PNG',
    // showPosterWhenEnded:	true,
    // showPosterWhenPaused: true,
    // videoWidth:-1,
    // videoHeight:-1,
    features: ['playpause', 'progress', 'current', 'duration', 'tracks', 'volume', 'fullscreen'],
    pluginPath: '/assets/mejs/swf/',
    success: function(mediaElement, originalNode) {
      console.log('Initialized');
    }
  };

  private play = {
    features: ['playpause', 'progress', 'current', 'duration', 'tracks', 'volume', 'fullscreen'],
    pluginPath: '/assets/mejs/swf/',
    success: function(mediaElement, originalNode) {
      console.log('Initialized');
    }
  };

  constructor(  private route: ActivatedRoute,private spinner: NgxSpinnerService,) { }

  ngOnInit() {
    this.spinner.show();  
    debugger;
    this.videoID = this.route.snapshot.paramMap.get('VideoID');

    switch (browser && browser.name) {
      case 'firefox':
      case 'chrome':

          // RTMP
        this.song = 'https://youtu.be/'+this.videoID;
        this.player = new MediaElementPlayer('player', this.playFlashBack);

        console.log('Chrome and Firefox - Player',this.playFlashBack);
        break;

      case 'edge':
      case 'safari':

        // HLS
        this.song = 'https://youtu.be/'+this.videoID;
        this.player = new MediaElementPlayer('player', this.play);

        console.log('Edge and Safari - Player');
        break;


      default:
        console.log('BROWSER UNDEFINED');
        break;
    }
    this.player.setSrc(this.song);
    this.player.load();
    this.spinner.hide(); 
  }

}
