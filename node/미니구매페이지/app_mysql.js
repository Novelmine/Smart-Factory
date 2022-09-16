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
    res.render('buy')
});
app.post('/result', async(req, res) => {
    obj = {
        name : req.body.name,
        product : req.body.product,
        buytime : new Date()
    }
    console.log(obj)
    req.body.param = obj
    await mysql.query('customerInsert', req.body.param);
    res.render('main');
});

app.get('/api/customersList', async(req, res) => {
    const customers = await mysql.query('customerlist');
    console.log(customers);
    var step;

    const obj = [];

for(step = 0; step < customers.length; step++){
    obj[step] ={
    id : customers[step].id  ,
    name: customers[step].name  ,
    product: customers[step].product,
    buytime: customers[step].buytime }
}
    console.log(obj[0]);
    res.render('result', {data: obj})
});
