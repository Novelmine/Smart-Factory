let puppeteer = require('puppeteer')
const express = require('express')
require('dotenv').config({path: 'mysql/.env'})

const mysql = require('./mysql')

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

app.get('/', async(req, res) => {
    let browser = await puppeteer.launch({headless:false});
    let page = await browser.newPage();
    await page.goto('https://www.coupang.com/np/campaigns/82')
    let eh = await page.$('dd.descriptions');
    let title = await eh.$eval('div.name', function(el){
        return el.innerText
    })
    let ehList = await page.$$('dd.descriptions');

    for(let eh of ehList){
        let title = await eh.$eval('div.name', function(el){
            return el.innerText
        })
        let price1 = await eh.$eval('strong.price-value', function(el){
            return el.innerText
        })
        obj = {
            name1 : title,
            price : price1
        }
        req.body.param = obj;
        await mysql.query('customerInsert', req.body.param);
    }
    browser.close()
    const customers = await mysql.query('customerlist');
    console.log(customers);
    var step;

    const obj1 = [];

for(step = 0; step < customers.length; step++){
    obj1[step] ={
    name1 : customers[step].name1  ,
    price  : customers[step].price
}
}
    res.render('main',{data: obj1});
});

