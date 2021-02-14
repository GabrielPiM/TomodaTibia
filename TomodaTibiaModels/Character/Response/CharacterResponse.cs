using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CSharp.RuntimeBinder;

namespace TomodaTibiaModels.Character.Response
{
    public class CharacterResponse
    {
        public CharacterResponse(dynamic nome, dynamic level, dynamic vocacao, dynamic sexo, dynamic isPremium)
        {
            this.nome = (string)nome;
            this.level = (int)level;
            this.vocacao = (string)vocacao;
            this.sexo = (string)sexo;
            this.gifChar = this.sexo + this.vocacao.Replace(" ", "");
            this.isPremium = isPremium == "Premium Account" ? true : false;
        }

        public CharacterResponse()
        { }

        public string nome { get; set; }
        public string gifChar { get; set; }
        public int level { get; set; }
        public string vocacao { get; set; }
        public string sexo { get; set; }
        public bool isPremium { get; set; }
    }
}
