const express = require('express')
const fs = require('fs');
const app = express();

app.set('view engine', 'ejs')
app.use(express.json({
    limit: '50mb'
}))
app.listen(3000, () => {
    console.log("Server Started port 3000...")
});

app.get('/', function(req, res){
    var array_result = []
    var cnt = 0
    var array = fs.readFileSync('C:/Users/Admin/Desktop/writeTest.txt').toString().split("\n")
    
    const obj = [
        {
            result: (`${array[array.length - 6]}`)
        },
        {
            result: (`${array[array.length - 5]}`)
        },
        {
            result: (`${array[array.length - 4]}`)
        },
        {
            result: (`${array[array.length - 3]}`)
        },
        {
            result: (`${array[array.length - 2]}`)
        }
    ]
    console.log(obj)
    res.render('result', {data: obj})
});