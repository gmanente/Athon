function ativarTreeView(idElement) {
    // PAGE RELATED SCRIPTS		
    $(idElement).find('.tree > ul').attr('role', 'tree').find('ul').attr('role', 'group').find('li:not(.not-tree)').hide();
    $(idElement).find('.tree > ul').attr('role', 'tree').find('ul').attr('role', 'group');
    $(idElement).find('.tree > ul').find('li:not(.not-tree)').addClass('parent_li').attr('role', 'treeitem');

    //$('.tree > ul').attr('role', 'tree').find('ul').attr('role', 'group').find('li').hide();
    //$('.tree > ul').attr('role', 'tree').find('ul').attr('role', 'group');
    //$('.tree > ul').find('li').addClass('parent_li').attr('role', 'treeitem');      
    //$('.tree').find('li:has(ul)').addClass('parent_li').attr('role', 'treeitem').find(' > span').attr('title', 'Collapse this branch').on('click', function (e)
    //$('.tree').find('li:has(ul)').addClass('parent_li').attr('role', 'treeitem').find(' > span').on('click', function (e) {
    $(idElement).find('.tree').find('li:has(ul)').addClass('parent_li').attr('role', 'treeitem').find(' > span').on('click', function (e) {
        var children = $(this).parent('li.parent_li').find(' > ul > li');
        if (children.is(':visible')) {
            children.hide('fast');
            //$(this).attr('title', 'Expand this branch').find(' > i').removeClass().addClass('fa fa-lg fa-plus-circle');
            $(this).find('i.icon-main').removeClass().addClass('fa fa-lg fa-folder index icon-main');
            $(this).attr("state", "close");
        } else {
            children.show('fast');
            //$(this).attr('title', 'Collapse this branch').find(' > i').removeClass().addClass('fa fa-lg fa-minus-circle');
            $(this).find('i.icon-main').removeClass().addClass('fa fa-lg fa-folder-open index icon-main');
            $(this).attr("state", "open");
        }
        e.stopPropagation();
    });

    $(idElement).find(".tree-search").keyup(function () {
        var textFind = $(this).val().trim();        
        var charLen = textFind.length;
        var resultCount = 0;        
        if (charLen > 3) {
            console.log(idElement);
            $(idElement).find("li.parent_li").each(function (k, v) {    
                var keyText = ($(this).attr("k-text") || "");
                var textCompare = keyText.toLowerCase();
                textFind = textFind.toLowerCase();
                if (textCompare.includes(textFind)) {
                    resultCount++;
                    $(this).removeClass("tree-hide");
                    $(this).parents("li.line-main").removeClass("tree-hide");

                    if ($(this).hasClass("line-main")) {
                        if ($(this).find("> span").attr("state") != "open") {
                            $(this).find("> span").click();
                            $(this).find("> span").attr("state", "open");
                        }
                    }
                    else {
                        if ($(this).parents("li.line-main").find("> span").attr("state") != "open") {
                            $(this).parents("li.line-main").find("> span").click();
                            $(this).parents("li.line-main").find("> span").attr("state", "open");
                        }
                    }
                }
                else {
                    if ($(this).hasClass("line-main")) {
                        $(this).addClass("tree-hide");
                    }
                    else {
                        $(this).addClass("tree-hide");
                    }
                }
            });

            if ($(idElement).find("span[state=open]").parent().find("li").length == $(idElement).find("span[state=open]").parent().find("li.tree-hide").length)
                $(idElement).find("span[state=open]").parent().find("li").removeClass("tree-hide");

            if (resultCount == 0) {
                $(idElement).find("span[state=open]").click();
                $(idElement).find("span[state=open]").attr("state", "close")
            }
        }
        else {
            $(idElement).find(".parent_li.tree-hide").removeClass("tree-hide");
            $(idElement).find("span[state=open]").click();
            $(idElement).find("span[state=open]").attr("state", "close")
        }        
    });
}

/* EXEMPLO PARA USO COM CAMPO DE PESQUISA
    
    // HTML
    <div class="col-md-12" id="testando">
        <div class="form-group input" id="" style="">
            <input type="text" class="form-control tree-search" placeholder="Pesquisar nos resultados abaixo" />
            <i class="icon-append fa fa-search"></i>
        </div>
        <div class="tree smart-form clear">
            <ul>
                <li class="none line-main" k-text="Agora"><span class="tree-linha c-dark-blue index"><i class="fa fa-lg fa-folder"></i>&nbsp;&nbsp;Agora</span>
                    <ul>
                        <li k-text="Item1" class="grid-row"><a class="tree-item btn btn-newblue btn-xs"><i class="fa fa-arrow-circle-right"></i></a><span class='tree-item tree-span'>Item1</span></li>
                        <li k-text="Item2" class="grid-row"><a class="tree-item btn btn-newblue btn-xs"><i class="fa fa-arrow-circle-right"></i></a><span class='tree-item tree-span'>Item2</span></li>
                        <li k-text="Item3" class="grid-row"><a class="tree-item btn btn-newblue btn-xs"><i class="fa fa-arrow-circle-right"></i></a><span class='tree-item tree-span'>Item3</span></li>
                        <li k-text="Item4" class="grid-row"><a class="tree-item btn btn-newblue btn-xs"><i class="fa fa-arrow-circle-right"></i></a><span class='tree-item tree-span'>Item4</span></li>
                        <li k-text="Item5" class="grid-row"><a class="tree-item btn btn-newblue btn-xs"><i class="fa fa-arrow-circle-right"></i></a><span class='tree-item tree-span'>Item5</span></li>
                    </ul>
                </li>
                <li class="none line-main" k-text="Titulo"><span class="tree-linha c-dark-blue index"><i class="fa fa-lg fa-folder"></i>&nbsp;&nbsp;Titulo</span>
                    <ul>
                        <li k-text="Teste1" class="grid-row"><a class="tree-item btn btn-newblue btn-xs"><i class="fa fa-arrow-circle-right"></i></a><span class='tree-item tree-span'>Teste1</span></li>
                        <li k-text="Teste2" class="grid-row"><a class="tree-item btn btn-newblue btn-xs"><i class="fa fa-arrow-circle-right"></i></a><span class='tree-item tree-span'>Teste2</span></li>
                        <li k-text="Teste3" class="grid-row"><a class="tree-item btn btn-newblue btn-xs"><i class="fa fa-arrow-circle-right"></i></a><span class='tree-item tree-span'>Teste3</span></li>
                        <li k-text="Teste4" class="grid-row"><a class="tree-item btn btn-newblue btn-xs"><i class="fa fa-arrow-circle-right"></i></a><span class='tree-item tree-span'>Teste4</span></li>
                        <li k-text="Teste5" class="grid-row"><a class="tree-item btn btn-newblue btn-xs"><i class="fa fa-arrow-circle-right"></i></a><span class='tree-item tree-span'>Teste5</span></li>
                    </ul>
                </li>
                <li class="none line-main" k-text="Outro"><span class="tree-linha c-dark-blue index"><i class="fa fa-lg fa-folder"></i>&nbsp;&nbsp;Outro</span>
                    <ul>
                        <li k-text="Item1" class="grid-row"><a class="tree-item btn btn-newblue btn-xs"><i class="fa fa-arrow-circle-right"></i></a><span class='tree-item tree-span'>Item1</span></li>
                        <li k-text="Item2" class="grid-row"><a class="tree-item btn btn-newblue btn-xs"><i class="fa fa-arrow-circle-right"></i></a><span class='tree-item tree-span'>Item2</span></li>
                        <li k-text="Item3" class="grid-row"><a class="tree-item btn btn-newblue btn-xs"><i class="fa fa-arrow-circle-right"></i></a><span class='tree-item tree-span'>Item3</span></li>
                        <li k-text="Item4" class="grid-row"><a class="tree-item btn btn-newblue btn-xs"><i class="fa fa-arrow-circle-right"></i></a><span class='tree-item tree-span'>Item4</span></li>
                        <li k-text="Item5" class="grid-row"><a class="tree-item btn btn-newblue btn-xs"><i class="fa fa-arrow-circle-right"></i></a><span class='tree-item tree-span'>Item5</span></li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>

    // JAVASCRIPT
    ativarTreeView("#testando")

    */