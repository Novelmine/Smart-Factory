const { Console } = require('console');
const si = require('systeminformation');



si.cpu().then(function(data) {
    console.log(data.voltage)
  })