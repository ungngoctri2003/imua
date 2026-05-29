/**
 * @license Copyright (c) 2003-2021, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';
    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.languages = 'vi';
    config.filebrowserBrowseUrl = '/Areas/Admin/assets/js/plugin/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Areas/Admin/assets/js/plugin/ckfinder/ckfinder.html?Types=Images';
    config.filebrowserFlashBrowseUrl = '/Areas/Admin/assets/js/plugin/ckfinder/ckfinder.html?Types=Flash';
    config.filebrowserUploadUrl = '/Areas/Admin/assets/js/plugin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=File';
    config.filebrowserImageUploadUrl = '/Areas/Admin/assets/js/plugin/Data';
    config.filebrowserFlashUploadUrl = '/Areas/Admin/assets/js/plugin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

    CKFinder.setupCKEditor(null, '/Areas/Admin/assets/js/plugin/ckfinder/');
};
