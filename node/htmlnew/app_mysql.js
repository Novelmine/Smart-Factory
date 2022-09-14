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
app.get('/test3', function(req, res){
    res.render('test3')
});
app.get('/api/customers', async(req, res) => {
    const customers = await mysql.query('customerList');
    console.log(customers);
    res.send(customers);
});


app.post('/result', async(req, res) => {
    obj = {
        name : req.body.name,
        email : req.body.email,
        address : req.body.address
    }
    console.log(obj)
    req.body.param = obj
    const result = await mysql.query('customerInsert', req.body.param);
    res.render('main');
});

app.get('/api/customersList', async(req, res) => {
    const customers = await mysql.query('customerlist');
    console.log(customers);
    var step;
    var html = "";
    for (step = 0; step < customers.length; step++)
    {
        html += 
        `
        <table border="1">
            <tr>
                <td>아이디</td>
                <td>${customers[step].id}</td>
            </tr>
            <tr>
                <td>이름</td>
                <td>${customers[step].name}</td>
            </tr>
            <tr>
                <td>이메일</td>
                <td>${customers[step].email}</td>
            </tr>
            <tr>
                <td>주소</td>
                <td>${customers[step].address}</td>
            </tr>
        </table>`;
    };
    html += `<a href="http://localhost:3000">
    <input type="button" value="확인"></input>
    </a>`;
    res.send(html);
});

app.post('/api/update', async(req,res) => {
    var obj1 = [];
    if (req.body.part1 == `name`)
    {
        obj1[0] = {
            name : req.body.part2,
        }
    }
    if (req.body.part1 == `address`)
    {
        obj1[0] = {
            address : req.body.part2,
        }
    }
    if (req.body.part1 == `email`)
    {
        obj1[0]= {
            email : req.body.part2,
        }
    }
    obj1[1] = req.body.id2  ;
    
    console.log(obj1)
    req.body.param = obj1
    const result = await mysql.query('customerUpdate' , req.body.param);
    res.render('main');
});


app.post('/api/delete', async(req,res) => {
    req.body.param = req.body.id3 ;
    const result = await mysql.query('customerDelete', req.body.param );
    res.render('main');
});