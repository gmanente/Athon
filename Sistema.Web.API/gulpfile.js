/// <binding />
'use strict';
/**
 *  ===== GULP INSTALL =====
 *
 *  INSTALAR OS PACOTES
 *  $ npm install -g gulp gulp-cli
 *  $ npm install
 *
 *  APÓS INSTALAÇÃO, ADICIONAR O GULP NA VARIAVEL DE AMBIENTE
 *  $ PATH = %PATH%; %APPDATA%\npm
 *
 */

// Include gulp
var gulp = require('gulp');

// Include gulp plugins
var $ = require('gulp-load-plugins')();
var gutil = require('gulp-util');
var plumber = require('gulp-plumber');
var rename = require('gulp-rename');
var uglify = require('gulp-uglify');


/**
 *   Minify JS
 */
gulp.task('minify', function () {
    return gulp.src('sandbox/ui/lib/swagger.js')
        .pipe($.plumber())
        .pipe($.uglify({ output: { quote_style: 3 } }))
        .pipe($.rename({ suffix: '.min' }))
        .pipe(gulp.dest('sandbox/ui/lib/')) // compressed
        .on('error', gutil.log);
});
