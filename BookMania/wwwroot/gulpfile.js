const gulp = require('gulp');
const sass = require('gulp-sass');
const browserSync = require('browser-sync');

// compile scss into css
// name of gulp task
function style() {
    // 1. find the scss file
    return gulp.src('./scss/**/*.scss')
        // 2. pass that file through sass compiler
        .pipe(sass({ outputStyle: "compact" }).on('error', sass.logError))
        // 3. save the compiled CSS
        .pipe(gulp.dest('./css'))
        // 4. stream changes to all browsers
        .pipe(browserSync.stream());
}

function watch() {
    browserSync.init({
        browser: 'chrome',
        proxy: 'https://localhost:5001/'
    });
    gulp.watch('./scss/**/*.scss', style);
    gulp.watch(
        ['**/*.cshtml', '**/*.js', '*/*.ts', 'browsersync-update.txt'].map(x => '../' + x)
    ).on('change', browserSync.reload);
    //gulp.watch('./js/**/*.js').on('change', browserSync.reload);
}

exports.style = style;
exports.watch = watch;

