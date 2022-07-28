# Configuration file for the Sphinx documentation builder.
#
# This file only contains a selection of the most common options. For a full
# list see the documentation:
# https://www.sphinx-doc.org/en/master/usage/configuration.html

# -- General setup -----------------------------------------------------------

# We do far too much in this config file really... but what can you do?

import os
import re
import subprocess
import datetime
from typing import Any, Dict
from pygments import highlight

project_root = os.path.abspath('../../../')

# -- Path setup --------------------------------------------------------------

# If extensions (or modules to document with autodoc) are in another directory,
# add these directories to sys.path here. If the directory is relative to the
# documentation root, use os.path.abspath to make it absolute, like shown here.
#
# import os
# import sys
# sys.path.insert(0, os.path.abspath('.'))

# -- Project information -----------------------------------------------------

project = 'XKCP.NET'
copyright = '2022, Stirling Labs, all rights reserved.'
author = 'Stirling Labs'
html_logo = project_root + "/docs/XKCP-Anna-banner-dotnet.svg"

# The full version, including alpha/beta/rc tags
version = "GitHub"
release = version


# -- General configuration ---------------------------------------------------

# Add any Sphinx extension module names here, as strings. They can be
# extensions coming with Sphinx (named 'sphinx.ext.*') or your custom
# ones.
extensions = [
    'sphinx.ext.duration',
    "sphinx.ext.todo",
    "sphinx.ext.viewcode",
    "sphinx_multiversion",
    "myst_parser",
    "sphinxext.opengraph",
    'breathe',
    'sphinx_csharp',
    'sphinx.ext.intersphinx',
    'sphinx.ext.autosectionlabel',
    'sphinx.ext.coverage',
    'sphinx.ext.mathjax',
    'sphinx.ext.ifconfig',
    'sphinx.ext.viewcode',
    'sphinx.ext.inheritance_diagram',
    'sphinx.ext.githubpages',
]

# List of patterns, relative to source directory, that match files and
# directories to ignore when looking for source files.
# This pattern also affects html_static_path and html_extra_path.
exclude_patterns = ['_build', 'Thumbs.db', '.DS_Store']

# highlight_language = "c#"

# -- Breathe configuration -------------------------------------------------

breathe_projects = {
	"default": (project_root + "/docs/Doxygen/xml/")
}
breathe_default_project = "default"
breathe_default_members = ('members', 'undoc-members')

# -- CSharp configuration ----------------------------------------------------

# Are other languages used in the sphinx project, if yes add language (domain) prefix to reference labels
sphinx_csharp_multi_language = False

# Should generated external links be tested for validity
sphinx_csharp_test_links = False

# Remove these common prefixes from labels
sphinx_csharp_shorten_type_prefixes = [
    'System.',
    'System.IO',
]

# Do not create cross references for these standard/build-in types
sphinx_csharp_ignore_xref = [
    'string',
    'Vector2',
    'Vector3',
    'Span', 'BigSpan', 'nuint', 'ReadOnlySpan', 'ReadOnlyBigSpan', 'IEquatable', 'nint', 'IEqualityComparer', 'IComparer', 'BufferType'
]

# How to generate external doc links, replace %s with type. Use the format
#    'package name': ('direct link to %s', 'alternate backup link or search page')
sphinx_csharp_ext_search_pages = {
#    'upm.xrit': ('https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@0.9/api/%s.html',
#                 'https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@0.9/?%s'),
}

# Types that are in an external package. Use the format
#   'package name': {
#      'Namespace1': ['Type1', 'Type2'],
sphinx_csharp_ext_type_map = {
#    'unity': {
#        '': ['MonoBehaviour', 'ScriptableObject'],
#        'XR': ['InputDevice', 'InputDeviceCharacteristics'],
#    },
}

# [Advanced] Rename type before generating external link. Commonly used for generic types
sphinx_csharp_external_type_rename = {
}

# Debug options, these enable various verbose logging features.
sphinx_csharp_debug = False # enables all debug options
sphinx_csharp_debug_parse = False # enables all parsing options
sphinx_csharp_debug_parse_func = False
sphinx_csharp_debug_parse_var = False
sphinx_csharp_debug_parse_prop = False
sphinx_csharp_debug_parse_attr = False
sphinx_csharp_debug_parse_idxr = False
sphinx_csharp_debug_parse_type = False
sphinx_csharp_debug_xref = False
sphinx_csharp_debug_ext_links = False

# -- Options for TODOs -------------------------------------------------------
#

todo_include_todos = True

# -- Options for Markdown files ----------------------------------------------
#

myst_enable_extensions = [
    "colon_fence",
    "deflist",
]
myst_heading_anchors = 3

# -- Options for HTML output -------------------------------------------------

html_title = "XKCP.NET"
language = "en"

# The theme to use for HTML and HTML Help pages.  See the documentation for
# a list of builtin themes.
#
html_theme = 'furo'

# Add any paths that contain custom static files (such as style sheets) here,
# relative to this directory. They are copied after the builtin static files,
# so a file named "default.css" will overwrite the builtin "default.css".
html_static_path = ['_static']

# Controls whether you see the project’s name in the sidebar of the documentation. 
# This is useful when you only want to show your documentation’s logo in the sidebar. 
# The default is False.
html_theme_options = {
    "sidebar_hide_name": True,
}

templates_path = ["_templates"]

html_sidebars = {
    "**": [
        "sidebar/brand.html",
        "sidebar/search.html",
        "sidebar/version.html",
        "sidebar/scroll-start.html",
        "sidebar/navigation.html",
        "sidebar/scroll-end.html",
        "sidebar/sphinx-multiversion.html",
    ]
}

html_theme_options: Dict[str, Any] = {
    "footer_icons": [
        {
            "name": "GitHub",
            "url": "https://github.com/StirlingLabs/XKCP.NET",
            "html": """
                <svg stroke="currentColor" fill="currentColor" stroke-width="0" viewBox="0 0 16 16">
                    <path fill-rule="evenodd" d="M8 0C3.58 0 0 3.58 0 8c0 3.54 2.29 6.53 5.47 7.59.4.07.55-.17.55-.38 0-.19-.01-.82-.01-1.49-2.01.37-2.53-.49-2.69-.94-.09-.23-.48-.94-.82-1.13-.28-.15-.68-.52-.01-.53.63-.01 1.08.58 1.23.82.72 1.21 1.87.87 2.33.66.07-.52.28-.87.51-1.07-1.78-.2-3.64-.89-3.64-3.95 0-.87.31-1.59.82-2.15-.08-.2-.36-1.02.08-2.12 0 0 .67-.21 2.2.82.64-.18 1.32-.27 2-.27.68 0 1.36.09 2 .27 1.53-1.04 2.2-.82 2.2-.82.44 1.1.16 1.92.08 2.12.51.56.82 1.27.82 2.15 0 3.07-1.87 3.75-3.65 3.95.29.25.54.73.54 1.48 0 1.07-.01 1.93-.01 2.2 0 .21.15.46.55.38A8.013 8.013 0 0 0 16 8c0-4.42-3.58-8-8-8z"></path>
                </svg>
            """,
            "class": "",
        },
    ],
}

# -- Options for extlinks ----------------------------------------------------
#

extlinks = {
#    "StirlingLabs": ("https://StirlingLabs.github.io/%s/", ""),
}

# -- Options for intersphinx -------------------------------------------------
#

intersphinx_mapping = {
#    "SafeShm": ("https://StirlingLabs.github.io/SafeShm", None),
#    "BigSpans": ("https://StirlingLabs.github.io/BigSpans", None),
}

# -- Opengraph settings -----------------------------------------------------

# This config option is very important, set it to the URL the site is being hosted on.
ogp_site_url = "https://stirlinglabs.github.io/XKCP.NET"
# Link to image to show (optional). Note that  relative paths are converted to be relative to the root of the html output
ogp_image = "https://stirlinglabs.github.io/libsa/main/_static/libsa-social.jpg"
# Optional alt-text for image. Defaults to using ogp_site_name or the document's title, if available.
# Set to False if you want to turn off alt text completely.
ogp_image_alt = "XKCP.NET"
# Name of the site, displayed above the title (optional).
ogp_site_name = "XKCP.NET"
# Configure the amount of characters taken from a page. The default of 200 is probably good for most people.
# ogp_description_length = 300
# Name of the site, not required but displayed above the title.

# -- Multi-Version settings -------------------------------------------------

# Whitelist pattern for tags (set to None to ignore all tags)
smv_whitelist_tags = r'^v\d+\.\d+\.\d+$'
# Pattern for released versions
smv_released_pattern = r'^tags/v\d+\.\d+\.\d+$'

