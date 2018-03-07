'use strict';
var express = require('express');
var router = express.Router();

var Request = require('tedious').Request;
var Connection = require('tedious').Connection;

// Create connection to database
var config =
    {
        userName: 'sheepshead', // update me
        password: 'Our capstone database.', // update me
        server: 'capstone-sheepshead.database.windows.net', // update me
        options:
            {
                database: 'Sheepshead', //update me
                encrypt: true,
                rowCollectionOnDone: true 
            }
    }


/* GET cards. */
router.get('/', function (req, res) {
    res.contentType('application/json');	
    var connection = new Connection(config);

    var sql = "SELECT * FROM Card";
    //var returnData;

    // Attempt to connect and execute queries if connection goes through
    connection.on('connect', function (err) {
        if (err) {
            console.log(err);
            console.log("Request Error");
        }
        else {
            queryDatabase(null, function (err, rows) {

                console.log("Before res.send:" + rows);
                res.send(rows);
            });
        }
    });

    function queryDatabase(data, callback) {
        console.log('Reading rows from the Card Table...');
        var ret = [];


        // Read all rows from table
        var request = new Request(sql , function (err, rowCount, rows) {
                if (err) {
                    console.log(err);
                } else {
                    callback(null, ret);
                }
        });

        request.on('row', function (columns) {
            var rowObject = new Object();
            columns.forEach(function (column) {
                rowObject[column.metadata.colName] = column.value;
            });
            //console.log(rowObject);
            ret.push(rowObject);
        });

        connection.execSql(request);
    }

});

module.exports = router;