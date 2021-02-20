const webpackConfig = require('./webpack.config.js');

var paths = {
    webroot: "./wwwroot/",
    sass: "./wwwroot/sass/",
    css: "./wwwroot/css/",
    game: "./wwwroot/js/game"
};

module.exports = function (grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        sass: {
            options: {
                sourceMap: false, // Create source map
                style: 'compressed'
                
            },
            dist: {
                files: [
                    {
                        expand: true, // Recursive
                        cwd: paths.sass, // The startup directory
                        src: ["**/*.scss"], // Source files
                        dest: paths.css, // Destination
                        ext: ".css" // File extension
                    }
                ]
            }
        },
        webpack: {
            myconfig: webpackConfig
        },
        watch: {
            options: {
                livereload: true,
            },
            sass: {
                files: [paths.sass + "**/*scss"],
                tasks: ["sass"]
            },
            webpack: {
                files: [paths.game + "**/*js"],
                tasks: ["webpack"]
            }  
        }
    });

    grunt.loadNpmTasks("grunt-contrib-sass");
    grunt.loadNpmTasks("grunt-contrib-watch");
    grunt.loadNpmTasks("grunt-webpack");
    grunt.registerTask('default', ['watch']);

};
