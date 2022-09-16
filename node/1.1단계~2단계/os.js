const express = require('express');
const fs = require('fs');
const os = require('os');
const path = require('path');
const pr = require('process');
var test = os.cpus();
fs.rename('C:/Users/Admin/Code/WebApp/node/Mysql/09/access.log' , 'C:/Users/Admin/Code/WebApp/node/Mysql/09/info.log',function(err) {
    if ( err ) console.log('ERROR: ' + err);
});

var text = 
'호스트이름 : ' + os.hostname() +'\n' +
'메모리용량 : ' + os.totalmem() +'\n' +
'사용가능메모리 : ' + os.freemem() +'\n';

for (var i = 0; i<8 ;i++)
{
    text += 'CPU정보 : ' + test[i].model +'\n'
          + 'CPU정보 : ' + test[i].speed +'\n'
          + 'CPU정보 : ' + test[i].times.user +'\n'
          + 'CPU정보 : ' + test[i].times.nice +'\n'  
          + 'CPU정보 : ' + test[i].times.sys +'\n'  
          + 'CPU정보 : ' + test[i].times.idle +'\n'  
          + 'CPU정보 : ' + test[i].times.irq +'\n'           
}

fs.writeFile('C:/Users/Admin/Code/WebApp/node/Mysql/09/info.log', text
, err => {
    if (err) {
      console.error(err)
      return
    }

  })