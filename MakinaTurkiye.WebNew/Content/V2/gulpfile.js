var gulp = require('gulp');
var concat = require('gulp-concat');
var minify = require('gulp-minify');
var cleanCss = require('gulp-clean-css');
var rev = require('gulp-rev');
var bom = require('gulp-bom');
var criticalCss = require('gulp-critical-css');
var cmq = require('gulp-combine-media-queries');
var less = require('gulp-less');

 
gulp.task('pack-js-plugins', function () {	

    return gulp.src([
			'assets/js/bootstrap.min.js',
			'assets/jquery-ui-1.12.1.custom/jquery-ui.min.js',
			'assets/js/jquery.validationEngine.js',
			'assets/js/jquery.validationEngine-tr.js',
			'assets/js/jquery.unobtrusive-ajax.min.js',
			'assets/js/jquery.zoom.min.js',
            'assets/js/ResizeSensor.js',
        'assets/js/jquery.sticky-sidebar.js',
        'assets/js/CKEditor/ckeditor.js',
            'assets/js/owl.carousel.js',
            'assets/js/summernote.min.js',
            'assets/js/jquery.autocomplete.js',
        'assets/js/bootbox.min.js',
        'assets/js/jquery.mask.min.js',
        'assets/js/lightbox.min.js',
        'assets/js/bootstrapValidator.js',
            '../jQuery-Cookie-Disclaimer/dist/jquery-cookie/jquery.cookie.js',
            '../jQuery-Cookie-Disclaimer/dist/jquery.cookieDisclaimer.js',
			'assets/js/FaceBoxMessage/facebox.js',
        '../FlvPlayer/flowplayer-3.2.4.js',
        'assets/js/bootstrap-validate.js',
        'assets/js/bootstrap-tagsinput.min.js',
    
        'assets/js/JQuery-dropdowncascading.js',
        'assets/js/JQuery-Post-dropdowncascading.js',
        'assets/js/jquery.maskMoney.js',
        'assets/js/jquery.metadata.js',
        'assets/js/jquery.validate.min.js',


        "assets/js/jquery.magnific-popup.min.js",
        "assets/js/bootstrap-select.min.js"

	])
        .pipe(bom())
		.pipe(concat('bundle-plugins.js'))
		.pipe(minify())
		.pipe(gulp.dest('public/build/js'));
});

gulp.task('pack-js-main', function () {	
    return gulp.src(['assets/js/site.js',
        'assets/js/MtFormsValidate.js',
        'assets/js/newmembership.js',
        'assets/js/MtAjaxCalls.js',
        'assets/js/membership.js'
    ])
        .pipe(bom())
		.pipe(concat('bundle-main.js'))
		.pipe(minify())
		.pipe(gulp.dest('public/build/js'));
});

 
gulp.task('pack-css', function () {	
	return gulp.src([
			'assets/css/bootstrap.css',
        'assets/css/font-awesome.css',
        'assets/css/flaticon.css',
            'assets/css/owl.carousel.css',
            'assets/css/makinaturkiye_fonticons.css',
			'assets/jquery-ui-1.12.1.custom/jquery-ui.min.css',
			'assets/css/validationEngine.jquery.css',
			'assets/css/summernote-bs3.css',
			'../FaceBoxMessage/faceboxstoreconnect.css',
			'assets/css/site.css',
            'assets/css/style-v2.css',
        'assets/css/company-list.css',
        'assets/css/bootstrapValidator.css',
            'assets/css/product-list.css',
            'assets/css/header.css',
            'assets/css/fast-access-bar.css',
            'assets/css/store-profile-header.css',
        'assets/css/lightbox.css',
        'assets/css/cookieDisclaimer.css',
        'assets/css/bootstrap-tagsinput.css',
        'assets/css/bootstrap-datepicker.min.css',
        'assets/css/magnific-popup.css',
        'assets/css/bootstrap-select.min.css'
	])
        //.pipe(cmq({
        //    log: true
        //}))
		.pipe(concat('bundle-main.css'))
        //.pipe(criticalCss())
        .pipe(cleanCss({
            level: {
                2: {
                    all: false, // sets all values to `false`
                    removeDuplicateRules: true // turns on removing duplicate rules
                }
            }
        }))
        .pipe(gulp.dest('public/build/css'));
});


gulp.task('less', function () {
    return gulp.src(['assets/less/*.less'])
        .pipe(less())
        .pipe(gulp.dest('assets/css'));
});
 
gulp.task('watch', function() {
    gulp.watch('assets/js/*.js', ['pack-js-plugins', 'pack-js-main']);
    gulp.watch('assets/css/*.css', ['pack-css']);
    gulp.watch('assets/less/*.less', ['less']);
});

gulp.task('default', ['watch','pack-js-plugins', 'pack-js-main', 'pack-css']);


