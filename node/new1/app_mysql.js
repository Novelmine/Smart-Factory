const express = require('express');
const mysql = require('./mysql');

const app = express();

app.listen(3000, () => {

    console.log("서버 스타트 포트 3000")
});

app.get('/', function(req, res){
    res.send('메인 화면')
});
app.get('/api/customers' , async(req, res) =>{
    const customers = await mysql.query('customerlist');
    console.log(customers);
    res.send(customers);
});
