const express = require('express')
//DB설정 정보 가져오기
require('dotenv').config({path: 'mysql/.env'})

const mysql = require('./mysql')

const ejs = require('ejs')

const app = express();
app.use(express.json());
app.use(express.urlencoded({extended: true}));

app.set('view engine', 'ejs')

app.use(express.json({
    limit: '50mb'
}))


app.listen(3000, () => {
    console.log("Server Started port 3000...")
});

app.get('/', function(req, res){
    res.render('main')
});
app.get('/test1', function(req, res){
    res.render('test1')
});
app.get('/test2', function(req, res){
    res.render('test2')
});

app.post('/result', async(req, res) => {
   var num_n = req.body.num1 *  req.body.num2;
   num_n += '입니다!' + `<a href="http://localhost:3000">
   <input type="button" value="확인"></input>
   </a>`;
   res.send(num_n)
});

app.post('/result2', async(req,res) => {
    var num_n1 = {num_1 :(Number(req.body.num21) +  Number(req.body.num22)) ,
                  num_2 :  (req.body.num31  -  req.body.num32)   ,
                  num_3 :  (req.body.num41  *  req.body.num42)   ,
                  num_4 :  (req.body.num41  /  req.body.num42)  }
             
                  req.body.param = num_n1;
    res.render('test3.ejs', {'data' : num_n1} )
});