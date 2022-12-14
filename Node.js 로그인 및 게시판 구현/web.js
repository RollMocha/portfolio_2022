const express = require('express');
const app = express();
const fs = require("fs");
const http = require("http");
const mysql = require("mysql");
const session = require('express-session');
const bodyParser = require("body-parser");
const PORT = 8001;
const ejs = require('ejs');
const cookie = require('cookie-parser');

let sign_cnt = 0;
let add_cnt = 0;

const db = mysql.createConnection({
  user:'root',
  password:'1111',
  database:'cafe24'
});

app.use(bodyParser.urlencoded({extended: false}));
// app.use(session({
//   secret: 'my key',	// 원하는 문자 입력
//   resave: true,
//   saveUninitialized: true,
// }))

app.set(express.static("views"));
app.set('view engine', 'ejs');


app.get("/", function(req, res) {
  fs.readFile('/views/index.ejs', "utf-8", function(err, data) {
    res.send(data);
  });
});
app.post("/", function(req, res) {
    const{userID, userPassword} = req.body;
    console.log(userID);
    db.query("select * from member where userID = ? and userPassword = ?",[userID, userPassword], function(err,result){
      if (err) {
       console.log(err);
      }
      else if(result.length > 0){
        console.log("로그인 성공");
        res.redirect("/board");
      }
      else {
        res.send("<script>alert('로그인 실패'); window.history.back();</script>");
      }
    });
});
app.get("/sign_up", function(req, res) {
  fs.readFile('./views/sign_up.ejs', "utf-8", function(err, data) {
    res.send(data);
  });
});

app.post("/sign_up", function(req, res) {
    const {id, password, name, email} = req.body;
    db.query("insert into member(userID, userPassword, name, email) values (?, ?, ?, ?)", [id, password, name, email], function(err, result) {
      if (err) {
          console.log(err);
          return;
      } 
      else {
          res.redirect("/sign_up");
      }
  });
});
app.get("/board", function(req, res) {
  fs.readFile('./views/board.ejs', "utf-8", function(err, data) {
    db.query('select * from board', function(error, result){
      res.send(ejs.render(data, {result:result}));
    });
  });
});
app.post("/board", function(req, res) {
  const {box, search} = req.body;
  console.log(search);
  fs.readFile('./views/board.ejs', "utf-8", function(err, data){
    if(box == 2){
      db.query('select * from board where board_no = ?',[search], function(err, result){
        if(err){
          console.log(err);
        }
        else{
          res.send(ejs.render(data, {result:result}));
        }
      });
    }
    else if(box == 1){
      db.query('select * from board where board_title = ?',[search], function(err, result){
        if(err){
          console.log(err);
        }
        else{
          res.send(ejs.render(data, {result:result}));
        }
      });
    }
    else if(box == 0){
      db.query('select * from board', function(err, result){
        if(err){
          console.log(err);
        }
        else{
          res.send(ejs.render(data, {result:result}));
        }
      });
    }
    else{
      console.log("문제있음");
    }
  });
});

app.get("/add", function(req, res) {
  fs.readFile('./views/add.ejs', "utf-8", function(err, data) {
    res.send(data);
  });
});

app.post("/add", function(req, res) {
  const {title, content, password} = req.body;
  db.query("insert into cafe24.board(board_title, board_content, board_password) values (?, ?, ?)", [title, content, password], function(err, result) {
    if (err) {
        console.log(err);
        return;
    } 
    else {
        res.redirect("/board");
    }
  });
});

app.get("/space/:id", function(req, res) {
  fs.readFile("./views/space.ejs", "utf8", function(err, data) {
      db.query("select * from board where board_no = ?", [req.params.id], function(err, result) {
        if(err){
          console.log(err);
        }
        else{
          res.send(ejs.render(data, {result:result[0]}));
        }
      });
  });
});
app.post("/space/:id", function(req, res) {
  const {id,title, content, password} = req.body;
  db.query("select * from cafe24.board where board_password = ?",[password],function(err,result){
    if(err){
      console.log(err);
    }
    else if(result.length > 0){
      db.query("update cafe24.board set board_title = ?, board_content = ? where board_no = ?", [title, content, id], function(err) {
        if(err){
          console.log(err);
        }
        else{
          res.redirect("/board");
        }
      });
    }
    else{
      res.send("<script>alert('비밀번호가 틀립니다.'); window.history.back();</script>");
    }
  })
});
app.get("/delete/:id", function(req, res){
  fs.readFile('./views/delete.ejs', "utf8", function(err, data) {
    db.query("select * from board where board_no = ?", [req.params.id], function(err, result) {
      if(err){
        console.log(err);
      }
      else{
        res.send(ejs.render(data, {result:result[0]}));
      }
    });
  });
});
app.post("/delete/:id", function(req, res){
  const {id, password} = req.body;
  console.log(id);
  console.log(password);
  db.query("select * from cafe24.board where board_password = ?",[password],function(err,result){
    if(err){
      console.log(err);
    }
    else if(result.length > 0){
      console.log(result.length);
      db.query("delete from cafe24.board where board_no = ?", [id], function(err) {
        if(!err){
            res.redirect("/board");
        }
      });
    }
    else{
      res.send("<script>alert('비밀번호가 틀립니다.'); window.history.back();</script>");
    }
  });
});
app.listen(PORT, () => {
    console.log(`server started on PORT ${PORT}`);
});