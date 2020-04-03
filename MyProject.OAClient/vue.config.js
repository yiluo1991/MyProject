module.exports = {
    devServer: {
        proxy: {
            '^/api': {
                target: 'http://localhost:55395',//转发到该地址
                //配置对目录的重写规则
                pathRewrite: {
                    //把/api替换为空,^表示开头开始匹配
                    "^/api": ""
                },
                changeOrigin: true
            }
        }
    }
}