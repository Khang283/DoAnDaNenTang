const productsRoutes = require('./products');
const accountRoutes = require('./account');

function Routes(app){
    //PRODUCTS
    app.use('/products', productsRoutes);

    //ACCOUNT
    app.use('/account', accountRoutes);
}

module.exports=Routes;