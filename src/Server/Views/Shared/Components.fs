module Falco.Markup.Components

module Elem =
    let clickCounter = Elem.create "click-counter"

[<AutoOpen>]
module Html =
    let _clickCounter (attrs: XmlAttribute list) (children: XmlNode list) : XmlNode =
        Elem.clickCounter attrs children