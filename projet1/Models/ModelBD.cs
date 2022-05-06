 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
namespace projet1
{
    public class ModelBD : DbContext
    {
        // Votre contexte a été configuré pour utiliser une chaîne de connexion « ModelBD » du fichier 
        // de configuration de votre application (App.config ou Web.config). Par défaut, cette chaîne de connexion cible 
        // la base de données « projet1.ModelBD » sur votre instance LocalDb. 
        // 
        // Pour cibler une autre base de données et/ou un autre fournisseur de base de données, modifiez 
        // la chaîne de connexion « ModelBD » dans le fichier de configuration de l'application.
        public ModelBD()
            : base("name=ModelBD")
        {
        }

        public virtual DbSet<utilisateur> utlisateurs { get; set; }
        public virtual DbSet<client> clients { get; set; }
        public virtual DbSet<proprietaire> proprietaires { get; set; }
        public virtual DbSet<admin> admins { get; set; }
        public virtual DbSet<produit> produits { get; set; }
        public virtual DbSet<categorie> categories { get; set; }
        public virtual DbSet<commande> commandes { get; set; }
        //public virtual DbSet<Message> Messagers { get; set; }
        public virtual DbSet<reclamation> reclamations { get; set; }
        public virtual DbSet<particulier> particuliers { get; set; }
        public virtual DbSet<societe> societes { get; set; }
        public virtual DbSet<panier> paniers { get; set; }
        public virtual DbSet<panier_produit> paniers_produit { get; set; }
        public virtual DbSet<commande_produit> commandes_produits { get; set; }
        public virtual DbSet<conversation> conversations { get; set; }
        public virtual DbSet<historique> historiques { get; set; }
       
        public virtual DbSet<message> messages { get; set; }
       
        public class historique
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id_hist { get; set; }
            public string label { get; set; }
            public enum Operation { Ajout, Modification, Suppression }
            [DataType(DataType.Date)]
            public DateTime dateOperation { get; set; }
            public Operation operation { get; set; }
            [ForeignKey("utilisateur")]
            public int utilisateurId { get; set; }
            public virtual utilisateur utilisateur { get; set; }


        }
        public  class utilisateur
        {
           [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int id { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Entrez le prenom s'il vous plait")]
            public string nom { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Entrez le prenom s'il vous plait")]
            public string prenom { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Entrez votre email s'il vous plait")]
            [EmailAddress]
            public string email { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Entrez le numero de votre telephone s'il vous plait")]
            [Phone]
            public string tel { get; set; }
            //[Required(AllowEmptyStrings = false, ErrorMessage = "Entrez le mot de passe s'il vous plait")]
            public string password { get; set; }
           [Required(AllowEmptyStrings = false, ErrorMessage = "confirmez le mot de passe s'il vous plait")]
            [Compare("password")]
            public string passwordComfirm { get; set; }
            public DateTime dateAjout { get; set; }

            public string Role { get; set; }
            public string address { get; set; }
            public ICollection<message> messagesEnvoyés;
            public ICollection<message> messagesRecus;
            public utilisateur(utilisateur user)
            {
                this.nom = user.nom;
                this.prenom = user.prenom;
                this.email = user.email;
                this.password = user.password;
                this.tel = user.tel;
                this.address = user.address;
                this.passwordComfirm = user.passwordComfirm;
            }
            public utilisateur()
            {

            }

        }
        
        public class client : utilisateur
        {
           
            public enum Status {favorie,elimini,normal}
            public Status status { get; set; }
            public client()
            {

            }
            public client(utilisateur user) : base(user)
            {

            }
        }
        public class proprietaire : utilisateur
        { 
            public proprietaire(utilisateur user) : base(user)
            {

            }
            public proprietaire()
            {

            }
        }
        public class particulier : proprietaire
        {
            public particulier(proprietaire prop, string cin) : base(prop)
            {
                this.CIN = cin;
            }
            public particulier()
            {

            }
            public string CIN { get; set; }
        }
        public class societe : proprietaire
        {
            public societe(proprietaire prop, string siren, string type_activite) : base(prop)
            {
                this.SIREN = siren;
                this.type_activite = type_activite;
            }
            public societe()
            {

            }
            public string SIREN { get; set; }
            public string type_activite { get; set; }
        }
        public class admin : utilisateur
        {
            public admin()
            {

            }
            public admin(utilisateur user) : base(user)
            {

            }
        }
        public class produit
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int ref_Id { get; set; }
            //[Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez donner un libellé")]
            public string title { get; set; }
           // [Required(AllowEmptyStrings = false, ErrorMessage = "Entrez la quantité que vous souhaitez ajouter")]
            public int QtéStock { get; set; }
            public string subtitle { get; set; }
            [DataType(DataType.Date)]
            public DateTime dateAjout { get; set; }
            public enum StatusProduit { Vendu, NonVendu }
            public StatusProduit statusP { get; set; }
            //[Required(AllowEmptyStrings = false, ErrorMessage = "Donnez une description de produit")]
            public string description { get; set; }
           // [Required(AllowEmptyStrings = false, ErrorMessage = "Quel est le prix de votre produits")]
            public double prix { get; set; }
            [DisplayName("Upload Image")]
            public string ImagePath { get; set; }
            //[Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez donner une image descriptive de produit")]
            [NotMapped]
            public HttpPostedFileBase ImageFile { get; set; }
           // [Required(AllowEmptyStrings = false, ErrorMessage = "A quelle catégorie appartient le produit")]
            [ForeignKey("categorie"), Column(Order = 1)]
            public int CategorieId { get; set; }
            public virtual categorie categorie { get; set; }

            [ForeignKey("utilisateur")]
            public int utilisateurId { get; set; }
            public virtual utilisateur utilisateur { get; set; }


        }
        public class categorie
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int cat_Id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public ICollection<produit> produits { get; set; }
        }
        public class commande
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int reference_cmd { get; set; }
            public string date { get; set; }
            public string label { get; set; }
            public double montant { get; set; }
            public int quantite { get; set; }

            [ForeignKey("client")]
            public int clientId { get; set; }
            public virtual client client { get; set; }
           

        }
        public class emetteur : utilisateur
        {
            public emetteur(utilisateur user) : base(user)
            {

            }
            public emetteur()
            {

            }
        }
        public class recepteur : utilisateur
        {
            public recepteur(utilisateur user) : base(user)
            {

            }
            public recepteur()
            {

            }
        }
       
        public class reclamation
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int id { get; set; }
            public string header { get; set; }
        
            public string text { get; set; }
            [ForeignKey("commande")]
            public int CommandeId { get; set; }
            public virtual commande commande { get; set; }
            [ForeignKey("client")]
            public int clientId { get; set; }
            public virtual  client client { get; set; }
            [ForeignKey("utilisateur")]
            public int utilisateurId { get; set; }
            public virtual utilisateur utilisateur { get; set; }
            public string status { get; set; }
        }

        public class panier
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int id { get; set; }
            public double somme { get; set; }
            [ForeignKey("client")]
            public int client_Id { get; set; }
            public client client { get; set; }
        }
        public class panier_produit
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int id { get; set; }
            [ForeignKey("panier"), Column(Order = 1)]
            public int panier_id { get; set; }
            [ForeignKey("produit"), Column(Order = 2)]
            public int produit_id { get; set; }
            public virtual panier panier { get; set; }
            public virtual produit produit { get; set; }
            public int quantity { get; set; }
            public string size { get; set; }
        }

        public class commande_produit
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int id { get; set; }
            [ForeignKey("commande"), Column(Order = 1)]
            public int cmd_id { get; set; }
            [ForeignKey("produit"), Column(Order = 2)]
            public int produit_id { get; set; }
            public int quantity { get; set; }
            public virtual commande commande { get; set; }
            public virtual produit produit { get; set; }
        }
        public class conversation
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int id { get; set; }
            [ForeignKey("client"), Column(Order = 1)]
            public int clientId { get; set; }
            [ForeignKey("proprietaire"), Column(Order = 2)]
            public int proprietaireId { get; set; }
            public virtual client client { get; set; }
            public virtual proprietaire proprietaire { get; set; }
            public string LastMessage { get; set; }
            public string LastMessageDate { get; set; }

        }
        public class message
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int id { get; set; }
            public string text { get; set; }
            public string date { get; set; }
            [ForeignKey("conversation")]
            public int conversationId { get; set; }
           public bool fromClient { get; set; }
            public virtual conversation conversation { get; set; }


        }

        // Ajoutez un DbSet pour chaque type d'entité à inclure dans votre modèle. Pour plus d'informations 
        // sur la configuration et l'utilisation du modèle Code First, consultez http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}