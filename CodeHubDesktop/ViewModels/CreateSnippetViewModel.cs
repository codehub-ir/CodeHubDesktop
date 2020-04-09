using CodeHubDesktop.Data.Services;
using CodeHubDesktop.Models;
using HandyControl.Controls;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace CodeHubDesktop.ViewModels
{
    public class CreateSnippetViewModel : BindableBase
    {
        private string _Error;
        public string Error
        {
            get => _Error;
            set => SetProperty(ref _Error, value);
        }
        private string _SnippetUrl;
        public string SnippetUrl
        {
            get => _SnippetUrl;
            set => SetProperty(ref _SnippetUrl, value);
        }

        private bool _IsEnabled = true;
        public bool IsEnabled
        {
            get => _IsEnabled;
            set => SetProperty(ref _IsEnabled, value);
        }

        private string _Snippet;
        public string Snippet
        {
            get => _Snippet;
            set => SetProperty(ref _Snippet, value);
        }

        private string _Subject;
        public string Subject
        {
            get => _Subject;
            set => SetProperty(ref _Subject, value);
        }

        private string _Detail;
        public string Detail
        {
            get => _Detail;
            set => SetProperty(ref _Detail, value);
        }

        private string _SelectedCode;
        public string SelectedCode
        {
            get => _SelectedCode;
            set => SetProperty(ref _SelectedCode, value);
        }

        private ObservableCollection<string> _LanguageList = new ObservableCollection<string>();
        public ObservableCollection<string> LanguageList
        {
            get => _LanguageList;
            set => SetProperty(ref _LanguageList, value);
        }

        public DelegateCommand ClearCommand { get; set; }
        public DelegateCommand CreateCommand { get; set; }

        public CreateSnippetViewModel()
        {
            ClearCommand = new DelegateCommand(OnClear);
            CreateCommand = new DelegateCommand(OnCreateSnippet);
            FillComboBox();
        }

        private async void OnCreateSnippet()
        {
            try
            {
                IsEnabled = false;
                CreateSnippetModel snippet = new CreateSnippetModel
                {
                    title = Subject,
                    detail = Detail,
                    language = SelectedCode.ToLower(),
                    script = Snippet,
                    error = Error
                };

                string json = JsonConvert.SerializeObject(snippet);
                StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
                string url = GlobalData.Config.APIBaseAddress;

                using HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(url, data);

                string result = response.Content.ReadAsStringAsync().Result;
                GetSnippetModel parse = JsonConvert.DeserializeObject<GetSnippetModel>(result);
                SnippetUrl = parse.link;
                if (GlobalData.Config.StoreSnippet)
                {
                    SnippetsModel entity = new SnippetsModel
                    {
                        Title = parse.title,
                        Detail = parse.detail,
                        Language = parse.language,
                        Link = parse.link,
                        PubDate = parse.pub_date,
                        Script = parse.script,
                        SId = parse.SID,
                        Error = parse.error
                    };
                    IDataService<SnippetsModel> dataService = new GenericDataService<SnippetsModel>();
                    await dataService.CreateSnippet(entity);
                }
                IsEnabled = true;
            }
            catch (Exception ex)
            {
                Growl.Error(ex.Message);
            }
            finally
            {
                IsEnabled = true;
            }
        }

        private void OnClear()
        {
            Error = Snippet = Subject = Detail = string.Empty;
        }

        internal void FillComboBox()
        {
            string[] languages = new string[] {
              "ABAP",
  "ABNF",
  "Ada",
  "ADL",
  "Agda",
  "Aheui",
  "autohotkey",
  "Alloy",
  "Ampl",
  "ANTLR",
  "ANTLR With ActionScript Target",
  "ANTLR With CPP Target",
  "ANTLR With C# Target",
  "ANTLR With Java Target",
  "ANTLR With ObjectiveC Target",
  "ANTLR With Perl Target",
  "ANTLR With Python Target",
  "ANTLR With Ruby Target",
  "ApacheConf",
  "APL",
  "AppleScript",
  "Arduino",
  "ActionScript",
  "ActionScript 3",
  "AspectJ",
  "aspx-cs",
  "aspx-vb",
  "Asymptote",
  "AmbientTalk",
  "Augeas",
  "AutoIt",
  "Awk",
  "Base Makefile",
  "Bash",
  "Batchfile",
  "BBC Basic",
  "BBCode",
  "BC",
  "Befunge",
  "BibTeX",
  "BlitzBasic",
  "BlitzMax",
  "BNF",
  "Boa",
  "Boo",
  "Boogie",
  "Brainfuck",
  "BST",
  "BUGS",
  "C",
  "c-objdump",
  "ca65 assembler",
  "cADL",
  "CAmkES",
  "CapDL",
  "Cap'n Proto",
  "CBM BASIC V2",
  "Ceylon",
  "Coldfusion CFC",
  "CFEngine3",
  "Coldfusion HTML",
  "cfstatement",
  "ChaiScript",
  "Chapel",
  "Charmci",
  "Cheetah",
  "Cirru",
  "Clay",
  "Clean",
  "Clojure",
  "ClojureScript",
  "CMake",
  "COBOL",
  "COBOLFree",
  "CoffeeScript",
  "Common Lisp",
  "Component Pascal",
  "Bash Session",
  "Debian Control file",
  "Coq",
  "C++",
  "cpp-objdump",
  "CPSA",
  "Crystal",
  "Crmsh",
  "Croc",
  "Cryptol",
  "C#",
  "Csound Orchestra",
  "Csound Document",
  "Csound Score",
  "CSS",
  "CSS+Django/Jinja",
  "CSS+Ruby",
  "CSS+Genshi Text",
  "CSS+Lasso",
  "CSS+Mako",
  "CSS+mozpreproc",
  "CSS+Myghty",
  "CSS+PHP",
  "CSS+Smarty",
  "Gherkin",
  "CUDA",
  "Cypher",
  "Cython",
  "D",
  "d-objdump",
  "Dart",
  "DASM16",
  "Delphi",
  "dg",
  "Diff",
  "Django/Jinja",
  "Docker",
  "MSDOS Session",
  "Darcs Patch",
  "DTD",
  "Duel",
  "Dylan",
  "Dylan session",
  "DylanLID",
  "Earl Grey",
  "Easytrieve",
  "EBNF",
  "eC",
  "ECL",
  "Eiffel",
  "Elixir",
  "Elm",
  "EmacsLisp",
  "E-mail",
  "ERB",
  "Erlang erl session",
  "Erlang",
  "Evoque",
  "xtlang",
  "Ezhil",
  "Factor",
  "Fantom",
  "Fancy",
  "Felix",
  "Fennel",
  "Fish",
  "Flatline",
  "FloScript",
  "Forth",
  "Fortran",
  "FortranFixed",
  "FoxPro",
  "Freefem",
  "F#",
  "GAP",
  "GAS",
  "Genshi",
  "Genshi Text",
  "GLSL",
  "Gnuplot",
  "Go",
  "Golo",
  "GoodData-CL",
  "Gosu",
  "Groff",
  "Groovy",
  "Gosu Template",
  "Haml",
  "Handlebars",
  "Haskell",
  "Hxml",
  "Hexdump",
  "HLSL",
  "HSAIL",
  "Hspec",
  "HTML",
  "HTML+Cheetah",
  "HTML+Django/Jinja",
  "HTML+Evoque",
  "HTML+Genshi",
  "HTML+Handlebars",
  "HTML+Lasso",
  "HTML+Mako",
  "HTML+Myghty",
  "HTML + Angular2",
  "HTML+PHP",
  "HTML+Smarty",
  "HTML+Twig",
  "HTML+Velocity",
  "HTTP",
  "Haxe",
  "Hybris",
  "Hy",
  "Inform 6 template",
  "Icon",
  "IDL",
  "Idris",
  "Elixir iex session",
  "Igor",
  "Inform 6",
  "Inform 7",
  "INI",
  "Io",
  "Ioke",
  "IRC logs",
  "Isabelle",
  "J",
  "JAGS",
  "Jasmin",
  "Java",
  "Javascript+mozpreproc",
  "JCL",
  "Julia console",
  "JavaScript",
  "JavaScript+Cheetah",
  "JavaScript+Django/Jinja",
  "JavaScript+Ruby",
  "JavaScript+Genshi Text",
  "JavaScript+Lasso",
  "JavaScript+Mako",
  "JavaScript+Myghty",
  "JavaScript+PHP",
  "JavaScript+Smarty",
  "JSGF",
  "JSON",
  "JSONBareObject",
  "JSON-LD",
  "Java Server Page",
  "Julia",
  "Juttle",
  "Kal",
  "Kconfig",
  "Kernel log",
  "Koka",
  "Kotlin",
  "Literate Agda",
  "Lasso",
  "Literate Cryptol",
  "Lean",
  "LessCss",
  "Literate Haskell",
  "Literate Idris",
  "Lighttpd configuration file",
  "Limbo",
  "liquid",
  "LiveScript",
  "LLVM",
  "LLVM-MIR",
  "LLVM-MIR Body",
  "Logos",
  "Logtalk",
  "LSL",
  "Lua",
  "Makefile",
  "Mako",
  "MAQL",
  "Mask",
  "Mason",
  "Mathematica",
  "Matlab",
  "Matlab session",
  "markdown",
  "MIME",
  "MiniD",
  "Modelica",
  "Modula-2",
  "Monkey",
  "Monte",
  "MOOCode",
  "MoonScript",
  "Mosel",
  "mozhashpreproc",
  "mozpercentpreproc",
  "MQL",
  "MiniScript",
  "Mscgen",
  "MuPAD",
  "MXML",
  "Myghty",
  "MySQL",
  "NASM",
  "NCL",
  "Nemerle",
  "nesC",
  "NewLisp",
  "Newspeak",
  "Angular2",
  "Nginx configuration file",
  "Nimrod",
  "Nit",
  "Nix",
  "Notmuch",
  "NSIS",
  "NumPy",
  "NuSMV",
  "objdump",
  "objdump-nasm",
  "Objective-C",
  "Objective-C++",
  "Objective-J",
  "OCaml",
  "Octave",
  "ODIN",
  "Ooc",
  "Opa",
  "OpenEdge ABL",
  "PacmanConf",
  "Pan",
  "ParaSail",
  "Pawn",
  "PEG",
  "Perl",
  "Perl6",
  "PHP",
  "Pig",
  "Pike",
  "PkgConfig",
  "PL/pgSQL",
  "Pony",
  "PostgreSQL SQL dialect",
  "PostScript",
  "Gettext Catalog",
  "POVRay",
  "PowerShell",
  "Praat",
  "Prolog",
  "Properties",
  "Protocol Buffer",
  "PowerShell Session",
  "PostgreSQL console (psql)",
  "Pug",
  "Puppet",
  "Python 2.x Traceback",
  "Python console session",
  "PyPy Log",
  "Python Traceback",
  "Python",
  "Python 2.x",
  "QBasic",
  "QML",
  "QVTO",
  "Racket",
  "Ragel",
  "Ragel in C Host",
  "Ragel in CPP Host",
  "Ragel in D Host",
  "Embedded Ragel",
  "Ragel in Java Host",
  "Ragel in Objective C Host",
  "Ragel in Ruby Host",
  "Raw token data",
  "Ruby",
  "Ruby irb session",
  "RConsole",
  "Rd",
  "ReasonML",
  "REBOL",
  "Red",
  "Redcode",
  "reg",
  "ResourceBundle",
  "Rexx",
  "RHTML",
  "Ride",
  "Relax-NG Compact",
  "Roboconf Graph",
  "Roboconf Instances",
  "RobotFramework",
  "RQL",
  "RSL",
  "reStructuredText",
  "TrafficScript",
  "Rust",
  "SARL",
  "SAS",
  "Sass",
  "SuperCollider",
  "Scala",
  "Scaml",
  "scdoc",
  "Scheme",
  "Scilab",
  "SCSS",
  "SmartGameFormat",
  "Shen",
  "ShExC",
  "Sieve",
  "Silver",
  "Slash",
  "Slim",
  "Slurm",
  "Smali",
  "Smalltalk",
  "Smarty",
  "Standard ML",
  "Snobol",
  "Snowball",
  "Solidity",
  "Debian Sourcelist",
  "SourcePawn",
  "SPARQL",
  "RPMSpec",
  "S",
  "SQL",
  "sqlite3con",
  "SquidConf",
  "Scalate Server Page",
  "Stan",
  "Stata",
  "Swift",
  "SWIG",
  "systemverilog",
  "TADS 3",
  "TAP",
  "TASM",
  "Tcl",
  "Tcsh",
  "Tcsh Session",
  "Tea",
  "Termcap",
  "Terminfo",
  "Terraform",
  "TeX",
  "Text only",
  "Thrift",
  "Todotxt",
  "TOML",
  "MoinMoin/Trac Wiki markup",
  "Treetop",
  "TypeScript",
  "Transact-SQL",
  "Tera Term macro",
  "Turtle",
  "Twig",
  "TypoScript",
  "TypoScriptCssData",
  "TypoScriptHtmlData",
  "ucode",
  "Unicon",
  "UrbiScript",
  "USD",
  "Vala",
  "VB.net",
  "VBScript",
  "VCL",
  "VCLSnippets",
  "VCTreeStatus",
  "Velocity",
  "verilog",
  "VGL",
  "vhdl",
  "VimL",
  "WDiff",
  "Web IDL",
  "Whiley",
  "X10",
  "XML",
  "XML+Cheetah",
  "XML+Django/Jinja",
  "XML+Ruby",
  "XML+Evoque",
  "XML+Lasso",
  "XML+Mako",
  "XML+Myghty",
  "XML+PHP",
  "XML+Smarty",
  "XML+Velocity",
  "Xorg",
  "XQuery",
  "XSLT",
  "Xtend",
  "XUL+mozpreproc",
  "YAML",
  "YAML+Jinja",
  "Zeek",
  "Zephir",
  "Zig"
            };
            string[] languagesUpper = languages.Select(s => s.ToUpperInvariant()).ToArray();
            LanguageList.AddRange(languagesUpper);
        }
    }
}
