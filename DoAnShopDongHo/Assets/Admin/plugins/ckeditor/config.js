/**
 * @license Copyright (c) 2003-2020, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';

    //config.extraPlugins = 'syntaxhighlight';
    config.syntaxhightlight_lang = 'cshap';
    config.syntaxhightlight_hideControls = true;
    config.language = 'vi';
    config.filebrowserBrowseUrl = '/Assets/Admin/plugins/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Assets/Admin/plugins/ckfinder/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '/Assets/Admin/plugins/ckfinder/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '/Assets/Admin/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&Type=File';
    config.filebrowserImageUploadUrl = '/Data';
    config.filebrowserFlashUploadUrl = '/Assets/Admin/plugins/ckfinder/core/connector/aspx/connector.aspx? command=QuickUpload&Type=Flash';
    CKFinder.setupCKEditor(null, '/Assets/Admin/plugins/ckfinder');
};
