const Account = require('../models/Account');

class AccountController{

    //[PUT] account/update
    update(req, res){
        const userId=req.body.userID;
        const data={
            username: req.body.username,
            //password: req.body.password,
            email: req.body.email,
            phone: req.body.phone,
            address: req.body.address,
        }
        Account.findByIdAndUpdate(userId,data,(err,account)=>{
            if(account){
                return res.status(200).send("Cập nhật tài khoản thành công");
            }
            else if(err){
                return res.status(500).send("Tên tài khoản đã tồn tại");
            }
            else{
                return res.status(404).send("Không tìm thấy tài khoản");
            }
        })
    }

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
            phone: req.body.phone,
            address: req.body.address,
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