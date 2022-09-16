const express = require('express');
const fs = require('fs');
const morgan = require('morgan');
const path = require('path');
// morgan 모듈 추가

const app =express();

const accessLogStream = fs.createWriteStream(path.join(__dirname, 'time.txt') ,{flags: 'a'}) 

app.use(morgan(':date', { stream: accessLogStream }))

app.listen(8000, () => {
  console.log('Running on port 3000');

});



app.get('/', function (req, res) {
  res.send('hello, world!')
})


    
