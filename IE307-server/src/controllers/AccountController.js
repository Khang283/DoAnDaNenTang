const Account = require('../models/Account');

class AccountController{

    //[POST] account/login
    login(req, res){
        let username=req.body.username;
        let password=req.body.password;
        console.log(username, password);
        Account.findOne({username: username, password: password},(err, account)=>{
            if(account){
                res.status(200).send("Đăng nhập thành công");
            }
            else if(err){
                res.status(500).send("Đã xảy ra lỗi");
            }
            else{
                res.status(404).send("Không tìm thấy tài khoản");
            }
        })
    }
}

module.exports = new AccountController();