var oracledb = require('oracledb');
var config = {
    user: "hr",
    password: "hr",
    connectString: "127.0.0.1/xe"
}

oracledb.getConnection(config, (err, conn) =>{
    todoWork(err, conn);
});

function todoWork(err, connection) {
    if (err) {
        console.error(err.message);
        return;
    }
    connection.execute("select * from jobs", [], function (err, result) {
        if (err) {
            console.error(err.message);
            doRelease(connection);
            return;
        }
        console.log(result.metaData);  //테이블 스키마
        console.log(result.rows);  //데이터
        doRelease(connection);
    });
}    

function doRelease(connection) {
    connection.release(function (err) {
        if (err) {
            console.error(err.message);
        }
    });
}
