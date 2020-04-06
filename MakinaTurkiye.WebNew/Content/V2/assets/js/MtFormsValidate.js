$(document).ready(function () {
    $('#add-user').bootstrapValidator({
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            memberemail: {
                selector: '#MemberEmail',
                message: 'Email Adresi Zorunludur',
                validators: {
                    notEmpty: {
                        message: 'Email adresi alanı zorunludur'
                    },
                    emailAddress: {
                        message: 'E-mail adresini doğru giriniz.'
                    },
                    remote: {
                        message: 'Bu Email adresi kullanılmaktadır',
                        url: '/Membership/CheckMemberEmail/'
                    }
                    
                }
            },
            password: {
                selector: '#MemberPassword',
                validators: {
                    notEmpty: {
                        message: 'Şifre alanı boş geçilemez'
                    },
                    stringLength: {
                        min: 6,
                        message: 'Şifre en az 6 karakterden oluşmalıdır'
                    }
                }
            },
            name: {
                selector: '#MemberName',
                validators: {
                    notEmpty: {
                        message: 'İsim  boş geçilemez'
                    }
                }
            },
            surname: {
                selector: '#MemberSurname',
                validators: {
                    notEmpty: {
                        message: 'Soyisim  boş geçilemez'
                    }
                }
            },
            gender: {
                selector: '.Gender',
                validators: {
                    notEmpty: {
                        message: 'Cinsiyet Boş Geçilemez'
                    }
                }
            },

        }
    });
    $('#login-form').bootstrapValidator({
        message: 'Bu değer valid değildir',
        feedbackIcons: {
            valid: 'fa fa-sync',
            invalid: 'fa fa-exclamation',
            validating: 'fa fa-check'
        },
        fields: {
            Email: {
                message: 'Email Adresi Zorunludur',
                validators: {
                    notEmpty: {
                        message: 'Email adresi alanı zorunludur'
                    },
                    emailAddress: {
                        message: 'E-mail adresini doğru giriniz.'
                    }

                }
            },
            Password: {
                validators: {
                    notEmpty: {
                        message: 'Şifre alanı boş geçilemez'
                    },
                    stringLength: {
                        min: 6,
                        message: 'Şifre en az 6 karakterden oluşmalıdır'
                    }
                }
            }
        }
    });
    $('#register-form').bootstrapValidator({
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'fa fa-sync',
            invalid: 'fa fa-exclamation',
            validating: 'fa fa-check'
        },
        fields: {
            MemberEmail: {
                message: 'Email Adresi Zorunludur',
                validators: {
                    notEmpty: {
                        message: 'Email adresi alanı zorunludur'
                    },
                    emailAddress: {
                        message: 'E-mail adresini doğru giriniz.'
                    },
                    remote: {
                        message: 'Bu Email adresi kullanılmaktadır',
                        url: '/Membership/CheckMemberEmail/'
                    }

                }
            },
            MemberPassword: {
                validators: {
                    notEmpty: {
                        message: 'Şifre alanı boş geçilemez'
                    },
                    stringLength: {
                        min: 6,
                        message: 'Şifre en az 6 karakterden oluşmalıdır'
                    }
                }
            },
            Name: {
                validators: {
                    notEmpty: {
                        message: 'İsim  boş geçilemez'
                    }
                }
            },
            Surname: {
                validators: {
                    notEmpty: {
                        message: 'Soyisim  boş geçilemez'
                    }
                }
            }
        }
    });


    $('#add-video-form').bootstrapValidator({
        message: 'This value is not valid',
        feedbackIcons: {

            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            VideoTitle: {
                validators: {
                    notEmpty: {
                        message: 'Başlık Giriniz'
                    }

                },


            },

            video: {
                validators: {
                    file: {
                        extension: 'mp4,mpeg mpg,m4v',
                        type: 'video/mp4,video/mpeg, video/x-m4v',
                        message: 'Dosya formatı uygun değil'
                    },
                    notEmpty: {
                        message: 'Video Seçiniz'
                    }
                }
            }
        }
    });


});

