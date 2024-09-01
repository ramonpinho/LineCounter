/** treeView.js
 * Author: Ramon Pinho
 * 
 * TreeView component
 * 
 * 
 * */

const TreeView = {
    /**
     * Creates a UI-TreeView component.
     * @param {*} domObj The id of a HTML container (preferrable a DIV) or its DOM object.
     * @param {*} jsonData The json containing the data to be rendered.
     * @param {*} css 
     */
    Create: function(domObj, jsonData, css) {
        var _domObj = null;

        if(typeof domObj == "string")
            _domObj = document.getElementById(domObj);
        else
            _domObj = domObj;

        var _data = null;
        if(typeof jsonData == "string")
            _data = JSON.parse(jsonData);
        else
            _data = jsonData;

        if(!css)
            css = "treeView";

        var _tree = {
            ParentObj: _domObj,
            Data: _data,
            Css: css,

            CssEven: css + Constants.CSS_EVEN,
            CssOdd: css + Constants.CSS_ODD,
            CssNoChild: css + Constants.CSS_NO_CHILD,
            CssWithChild: css + Constants.CSS_WITH_CHILD,
            // CssLevl2: css + Constants.CSS_LEVEL2,
            // CssLevl3: css + Constants.CSS_LEVEL3,
            CssLevel: css + Constants.CSS_LEVEL
        };

        if(_data.Root) {
            TreeView.Render(_tree);
            _domObj.Tree = _tree;
        }

        return _tree;
    },

    Render: function(tree) {

        if(tree.Css)
            tree.ParentObj.classList.add(tree.Css);

        for(var i = 0; i < tree.Data.Root.length; i++ ) {
            var node = tree.Data.Root[i];
            node.index = i;
            node.tree = tree;
            node.Level = 1;
            TreeView.RenderNode(tree.ParentObj, node);
        }
    },

    RenderNode: function(parent, node) {
        var _divContainer = document.createElement("div");
        parent.appendChild(_divContainer);
        // _divContainer.node = node;
        node.Container = _divContainer;

        var _div = document.createElement("div");
        _div.node = node; // used by onclick
        _divContainer.appendChild(_div);

        if(node.index%2==0)                
            _div.classList.add(node.tree.CssEven);
        else
            _div.classList.add(node.tree.CssOdd);

        if(node.Children && node.Children.length > 0) {
            _div.classList.add(node.tree.CssWithChild);            

            _div.onclick = function(e) {
                this.node.Expanded = !this.node.Expanded;
                TreeView.Toggle(this.node);
            }

            var _span = document.createElement("SPAN");
            _div.appendChild(_span);
            node.span = _span;

            TreeView.Toggle(node);
        }
        else
            _div.classList.add(node.tree.CssNoChild);

        _div.insertAdjacentText("beforeend", node.Description);
        
    },

    Toggle: function(node) {
        if(node.span) {
            if(node.Expanded) {                    
                node.span.innerHTML = "-";

                var _divChildren = document.createElement("div");
                node.Container.appendChild(_divChildren);
                node.divChildren = _divChildren;

                var nextLevel = node.Level + 1;
                var _cssLevel = node.tree.CssLevel + nextLevel;
                if(nextLevel>4)
                    _cssLevel = node.tree.CssLevel + "N";

                _divChildren.classList.add(_cssLevel);

                for(var i = 0; i< node.Children.length; i++)
                {
                    var child = node.Children[i];
                    if (child) {
                        child.tree = node.tree;
                        child.index = i;
                        child.Level = nextLevel;
                        TreeView.RenderNode(node.divChildren, child);
                    }
                }
                    
            }
            else {
                node.span.innerHTML = "+";
                if(node.divChildren) {
                    node.divChildren.remove();
                    node.divChildren = null;
                }                    
            }
                
        }
    }

    // RemoveChildren: function(node) {
    //     for(var i = 0; i< node.Children.length; i++) {
    //         var child = node.Children[i];
    //         if(child.div) {
    //             child.div.remove();
    //             child.div = null;
    //         }                
    //     }
    // }
}