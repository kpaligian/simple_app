module.exports = function (grunt) {
    'use strict';

    grunt.initConfig({
        watch: {
            source_ts: {
                files: ['Web/src/**/*.ts'],
                tasks: ['ts:src']
            },
            tests_ts: {
                files: ['Web/specs/src/**/*.ts'],
                tasks: ['ts:specs', 'jasmine']
            }
        },
        ts: {
            src: {
                src: ["Web/src/src.ts"], // The source typescript files, http://gruntjs.com/configuring-tasks#files
                out: 'Web/src/Src.js',         // If specified, generate an out.js file which is the merged js file     
                options: {
                    target: 'es3',            // 'es3' (default) | 'es5'
                    module: 'amd',       // 'amd' (default) | 'commonjs'
                    sourcemap: false,          // true  (default) | false
                    declaration: false,       // true | false  (default)                
                    comments: false           // true | false (default)
                }
            },

            specs: {
                src: ["Web/specs/**/*.ts"],
                reference: 'Web/specs/src/js_reference.ts',
                options: {
                    target: 'es3',
                    module: 'amd',
                    sourcemap: false,
                    declaration: false,
                    comments: false
                }
            }
        },
        jasmine: {
            src: ['Scripts/jquery-1.8.2.js',
                'Scripts/angular.min.js',
                'Scripts/angular-mocks.js',
                'Scripts/angular-ui-router.min.js',
                'Scripts/angular-resources.js',
                'Scripts/bootstrap.min.js',   
                'Scripts/src.js'],
            options: {
                verbose: true,
                specs: 'Web/specs/src/**/*Spec.js'
            }
        }
    });
    
    grunt.loadNpmTasks('grunt-ts');
    grunt.loadNpmTasks('grunt-contrib-jasmine');
    grunt.loadNpmTasks('grunt-contrib-watch');
	    
	grunt.registerTask('default', ['watch']);
}