const Account = require('../models/Account');

class AccountController{

    //[POST] account/login
    login(req, res){
        let username=req.body.username;
        let password=req.body.password;
        console.log(username, password);
        Account.findOne({username: username, password: password},(err, account)=>{
            if(account){
                res.status(200).json(account);
            }
            else if(err){
                res.status(500).send("Đã xảy ra lỗi");
            }
            else{
                res.status(404).send("Không tìm thấy tài khoản");
            }
        })
    }

    register(req, res){
        let data={
            username: req.body.username,
            password: req.body.password,
            email: req.body.email,
        }
        console.log(data);
        try{
            Account.findOne(data,(err, account)=>{
                if(account){
                    res.status(400).send("Tài khoản đã tồn tại !");
                }
                else if(err){
                    res.status(500).send("Lỗi Server !");
                }
                else{
                    const account = new Account(data);
                    account.save();
                    res.status(200).send("Tạo tài khoản thành công !");
                }
            });
        }
        catch{
            res.status(500).send("Lỗi Server !");
        }
    }
}

module.exports = new AccountController();