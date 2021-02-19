"use strict"

const path = require('path');

const ASSET_PATH = process.env.ASSET_PATH || '/';
module.exports = {
    entry: {
        e: "./wwwroot/js/game/Board.js"
    },
    optimization: {
        // We no not want to minimize our code.
        minimize: false
    },
    mode: 'production',
    output: {
        path: path.resolve(__dirname, './wwwroot/dist'),
        filename: "game.js",
        publicPath: ASSET_PATH,
    },

    module: {
        rules: [
            {
                test: /\.js?$/,
                exclude: /node_modules/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: [
                            "@babel/preset-env", {},
                        ],
                        plugins: [
                            ["@babel/plugin-proposal-class-properties"],
                        ]
                    }
                }
            }
        ]
    }
};