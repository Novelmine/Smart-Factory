var express = require('express');  
var app=express();  

app.set('view engine' , 'ejs');  //html template ejs로 사용하겠습니다.
app.set('views', './views');      //views 폴더를 ./views로 지정하겠습니다

app.use(express.static(__dirname + '/views'));

app.get('/',  (req, res) => {  
    res.render('main');  
 })  
 app.get('/result',  (req, res) => {  
    res.render('index',{
        name: req.query.name1 ,
        title: req.query.title1,
        content : req.query.content1
    });  
 })  
var server = app.listen(8000, function () {  
  var host = server.address().address  
  var port = server.address().port  
  console.log("Example app listening at http://%s:%s", host, port)  
})  
