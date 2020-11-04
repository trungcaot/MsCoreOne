module.exports = {
  transpileDependencies: ["vuetify"],
  configureWebpack:{
    optimization: {
      splitChunks: {
        cacheGroups: {
          node_vendors: {
            name:"vendor",
            test: /[\\/]node_modules[\\/](vuetify)[\\/]/,
            chunks: "all",
            priority: -10
          }
        }
      }
    }
  }
};