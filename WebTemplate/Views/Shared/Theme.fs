module Views.Shared.Theme

open Falco.Markup

let button (attrs: XmlAttribute list) (content: XmlNode list) =
    let attrs' =
        Attr.merge
            [ _class_
                  "inline-flex items-center justify-center rounded-full border border-lime-600 bg-lime-600 px-6 py-3 text-sm font-semibold text-white shadow-sm transition-colors hover:bg-lime-700 focus-visible:ring-4 focus-visible:ring-lime-200 focus-visible:outline-none" ]
            attrs

    _button attrs' content
