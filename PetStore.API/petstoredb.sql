--
-- PostgreSQL database dump
--

-- Dumped from database version 12.1
-- Dumped by pg_dump version 12.0

-- Started on 2020-01-31 10:16:03

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 7 (class 2615 OID 16548)
-- Name: petstore; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA petstore;


ALTER SCHEMA petstore OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 205 (class 1259 OID 16597)
-- Name: Category; Type: TABLE; Schema: petstore; Owner: postgres
--

CREATE TABLE petstore."Category" (
    "CategoryId" integer NOT NULL,
    "Name" character varying(255) NOT NULL
);


ALTER TABLE petstore."Category" OWNER TO postgres;

--
-- TOC entry 209 (class 1259 OID 16676)
-- Name: Category_CategoryId_seq; Type: SEQUENCE; Schema: petstore; Owner: postgres
--

CREATE SEQUENCE petstore."Category_CategoryId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE petstore."Category_CategoryId_seq" OWNER TO postgres;

--
-- TOC entry 2881 (class 0 OID 0)
-- Dependencies: 209
-- Name: Category_CategoryId_seq; Type: SEQUENCE OWNED BY; Schema: petstore; Owner: postgres
--

ALTER SEQUENCE petstore."Category_CategoryId_seq" OWNED BY petstore."Category"."CategoryId";


--
-- TOC entry 211 (class 1259 OID 16792)
-- Name: Comment; Type: TABLE; Schema: petstore; Owner: postgres
--

CREATE TABLE petstore."Comment" (
    "CommentId" integer NOT NULL,
    "Text" text NOT NULL,
    "ToyId" integer,
    "DatePosted" date DEFAULT CURRENT_DATE NOT NULL,
    "Author" character varying(20) NOT NULL
);


ALTER TABLE petstore."Comment" OWNER TO postgres;

--
-- TOC entry 210 (class 1259 OID 16790)
-- Name: Comment_CommentId_seq; Type: SEQUENCE; Schema: petstore; Owner: postgres
--

CREATE SEQUENCE petstore."Comment_CommentId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE petstore."Comment_CommentId_seq" OWNER TO postgres;

--
-- TOC entry 2882 (class 0 OID 0)
-- Dependencies: 210
-- Name: Comment_CommentId_seq; Type: SEQUENCE OWNED BY; Schema: petstore; Owner: postgres
--

ALTER SEQUENCE petstore."Comment_CommentId_seq" OWNED BY petstore."Comment"."CommentId";


--
-- TOC entry 203 (class 1259 OID 16561)
-- Name: Order; Type: TABLE; Schema: petstore; Owner: postgres
--

CREATE TABLE petstore."Order" (
    "OrderDate" date DEFAULT CURRENT_DATE NOT NULL,
    "OrderId" integer NOT NULL,
    "CustomerName" character varying(30) NOT NULL,
    "CustomerSurname" character varying(30) NOT NULL,
    "ShippingAddress" character varying(255) NOT NULL,
    "IPInfoAddress" character varying(255) NOT NULL,
    "OrderStatus" character varying(255) NOT NULL,
    "ExternalReferenceId" character varying(255)
);


ALTER TABLE petstore."Order" OWNER TO postgres;

--
-- TOC entry 204 (class 1259 OID 16578)
-- Name: OrderItem; Type: TABLE; Schema: petstore; Owner: postgres
--

CREATE TABLE petstore."OrderItem" (
    "Quantity" integer NOT NULL,
    "OrderId" integer NOT NULL,
    "ToyId" integer,
    "OrderItemId" integer NOT NULL
);


ALTER TABLE petstore."OrderItem" OWNER TO postgres;

--
-- TOC entry 206 (class 1259 OID 16643)
-- Name: OrderItem_OrderItemId_seq; Type: SEQUENCE; Schema: petstore; Owner: postgres
--

CREATE SEQUENCE petstore."OrderItem_OrderItemId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE petstore."OrderItem_OrderItemId_seq" OWNER TO postgres;

--
-- TOC entry 2883 (class 0 OID 0)
-- Dependencies: 206
-- Name: OrderItem_OrderItemId_seq; Type: SEQUENCE OWNED BY; Schema: petstore; Owner: postgres
--

ALTER SEQUENCE petstore."OrderItem_OrderItemId_seq" OWNED BY petstore."OrderItem"."OrderItemId";


--
-- TOC entry 207 (class 1259 OID 16651)
-- Name: Order_OrderId_seq; Type: SEQUENCE; Schema: petstore; Owner: postgres
--

CREATE SEQUENCE petstore."Order_OrderId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE petstore."Order_OrderId_seq" OWNER TO postgres;

--
-- TOC entry 2884 (class 0 OID 0)
-- Dependencies: 207
-- Name: Order_OrderId_seq; Type: SEQUENCE OWNED BY; Schema: petstore; Owner: postgres
--

ALTER SEQUENCE petstore."Order_OrderId_seq" OWNED BY petstore."Order"."OrderId";


--
-- TOC entry 202 (class 1259 OID 16551)
-- Name: Toy; Type: TABLE; Schema: petstore; Owner: postgres
--

CREATE TABLE petstore."Toy" (
    "Description" text NOT NULL,
    "CategoryId" integer,
    "ToyId" integer NOT NULL,
    "Name" character varying(30),
    "Price" money NOT NULL,
    "ShortDescription" character varying(50) NOT NULL,
    "Quantity" integer NOT NULL
);


ALTER TABLE petstore."Toy" OWNER TO postgres;

--
-- TOC entry 208 (class 1259 OID 16664)
-- Name: Toy_ToyId_seq; Type: SEQUENCE; Schema: petstore; Owner: postgres
--

CREATE SEQUENCE petstore."Toy_ToyId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE petstore."Toy_ToyId_seq" OWNER TO postgres;

--
-- TOC entry 2885 (class 0 OID 0)
-- Dependencies: 208
-- Name: Toy_ToyId_seq; Type: SEQUENCE OWNED BY; Schema: petstore; Owner: postgres
--

ALTER SEQUENCE petstore."Toy_ToyId_seq" OWNED BY petstore."Toy"."ToyId";


--
-- TOC entry 2719 (class 2604 OID 16678)
-- Name: Category CategoryId; Type: DEFAULT; Schema: petstore; Owner: postgres
--

ALTER TABLE ONLY petstore."Category" ALTER COLUMN "CategoryId" SET DEFAULT nextval('petstore."Category_CategoryId_seq"'::regclass);


--
-- TOC entry 2720 (class 2604 OID 16795)
-- Name: Comment CommentId; Type: DEFAULT; Schema: petstore; Owner: postgres
--

ALTER TABLE ONLY petstore."Comment" ALTER COLUMN "CommentId" SET DEFAULT nextval('petstore."Comment_CommentId_seq"'::regclass);


--
-- TOC entry 2717 (class 2604 OID 16653)
-- Name: Order OrderId; Type: DEFAULT; Schema: petstore; Owner: postgres
--

ALTER TABLE ONLY petstore."Order" ALTER COLUMN "OrderId" SET DEFAULT nextval('petstore."Order_OrderId_seq"'::regclass);


--
-- TOC entry 2718 (class 2604 OID 16645)
-- Name: OrderItem OrderItemId; Type: DEFAULT; Schema: petstore; Owner: postgres
--

ALTER TABLE ONLY petstore."OrderItem" ALTER COLUMN "OrderItemId" SET DEFAULT nextval('petstore."OrderItem_OrderItemId_seq"'::regclass);


--
-- TOC entry 2715 (class 2604 OID 16666)
-- Name: Toy ToyId; Type: DEFAULT; Schema: petstore; Owner: postgres
--

ALTER TABLE ONLY petstore."Toy" ALTER COLUMN "ToyId" SET DEFAULT nextval('petstore."Toy_ToyId_seq"'::regclass);


--
-- TOC entry 2869 (class 0 OID 16597)
-- Dependencies: 205
-- Data for Name: Category; Type: TABLE DATA; Schema: petstore; Owner: postgres
--

COPY petstore."Category" ("CategoryId", "Name") FROM stdin;
23	Dogs
24	Birds
25	Cats
\.


--
-- TOC entry 2875 (class 0 OID 16792)
-- Dependencies: 211
-- Data for Name: Comment; Type: TABLE DATA; Schema: petstore; Owner: postgres
--

COPY petstore."Comment" ("CommentId", "Text", "ToyId", "DatePosted", "Author") FROM stdin;
\.


--
-- TOC entry 2867 (class 0 OID 16561)
-- Dependencies: 203
-- Data for Name: Order; Type: TABLE DATA; Schema: petstore; Owner: postgres
--

COPY petstore."Order" ("OrderDate", "OrderId", "CustomerName", "CustomerSurname", "ShippingAddress", "IPInfoAddress", "OrderStatus", "ExternalReferenceId") FROM stdin;
\.


--
-- TOC entry 2868 (class 0 OID 16578)
-- Dependencies: 204
-- Data for Name: OrderItem; Type: TABLE DATA; Schema: petstore; Owner: postgres
--

COPY petstore."OrderItem" ("Quantity", "OrderId", "ToyId", "OrderItemId") FROM stdin;
\.


--
-- TOC entry 2866 (class 0 OID 16551)
-- Dependencies: 202
-- Data for Name: Toy; Type: TABLE DATA; Schema: petstore; Owner: postgres
--

COPY petstore."Toy" ("Description", "CategoryId", "ToyId", "Name", "Price", "ShortDescription", "Quantity") FROM stdin;
OTIS & CLAUDE BALLISTIC BUDDY BONE DOG TOY is a simple, yet elegant toy for your dog. The Ballistic Buddy Bone Dog Toy has a squeaker in a classic bone shape that your dog will love. The toy is made with heavy duty nylon exteriors for high-durability and is sewn together for extra strength. The edging is reinforced and a PVC packing ensures your dog won't destroy this toy easily. The toy is lined with 100% polyester and comes in a bright red color with beautiful black fabric on the back	23	38	Chew ball	$48.00	Earth friendly - The new Barkin Black color	96
KYJEN HARD CORE FIRE HOSE SQUEAK 'N FETCH is made from fire hose material, making it super durable. Fire hose material is designed for heavy-duty abrasion resistance, so this toy will be your dog's favorite for years! The tightly woven canvas shell and soft long-wearing rubber on the inside make this toy great for fetch. It's easy to throw and your dog will love to chew on the squeakers.	24	41	Kyjen Hard Core Fire Hose 	$52.00	Earth friendly - The new Barkin Black color	145
Solid Rubber Dog Chew Training Ball Toys Tooth Cleaning Chew Ball Puppy Pet Play Training Chewing Toy With Rope Handle	23	42	Solid Rubber Dog Chew 	$36.00	Very nice toy!	68
Over 100 years ago, in a very small town, Otis and Claude were building clever toys by hand. They used the best materials, and worked at their designs until they were extraordinary. Their handiwork lives on with Otis and Claude Fetching pet toys. The Ballistic Buddy line of toys utilize a strong fabric and simple designs to make a squeak toy that will last long.	24	39	Claude Ballistic Buddy	$48.00	Earth friendly - The new Barkin Black color	100
Set your dog out on a wild chase with these tough flyers! DOG TOY RINGS are especially made tough for high flying. They give dogs a great chase & a good exercise at the same time! Dog Toy Rings are designed with triple-stitched seams, heavy duty rope & canvas for long-lasting use. Our ring toys have been thoughtfully designed for loads of doggy fun. They fly, are soft & flexible. WOOF! Comes in 3 characters: Chuck-A-Duck®, Heave-A Beaver® or Hurl-A- Squirrel® - or collect 'em all!	25	40	Fat Cat Classic 	$48.00	Earth friendly - The new Barkin Black color	98
For the last 35 years and three generations, Safari Ltd. has been fostering curiosity, environmental conservation, and imagination in children around the world. We design and manufacture educational toys for school projects, play therapy sensory bin toys, animal toys, and even mythical creature figurines! We invite your family to take the Toys That Teach journey with us!\n	\N	50	Gummy crocodile	$234.00	One tree planted	423
Factory made Classic Pet Furniture Three Platform Luxury Toys Cat Tree Tower	\N	51	Climbing frame tree	$5.00	Eco-friendly, stocked	453
Natural sisal without used Jute, luxury sisal carped, E0 chipboard and MDF, all plush are used above 450 gram.	24	44	Pet Activity Center	$234.00	Factory made Classic Pet Furniture	43
Cheap wholesale premium quality pet scratching posts cat tree toy 	25	52	Pawz	$111.00	Eco-friendly, stocked	56
Classic Pet Product Small Cat Scratcher Toy, Cat Scratcher Post 	25	53	Cat Scratcher	$46.00	SISAL, Particle Board,Fur Plush and Sisal	150
Multi-Purpose IQ Treat Ball Dog Toy Squeak Pet Interactive Food Dispensing Ball Slow Feeder Pet Puzzle while Eating for Cat Dog	\N	54	Cat tree scratcher	$35.00	Particle Board, Fur Plush	79
Squeaky pet plush dog chew toy head stuffing china 	\N	55	Pet plush dog chew	$56.00	Plush fiber, 100% Polyester	250
\.


--
-- TOC entry 2886 (class 0 OID 0)
-- Dependencies: 209
-- Name: Category_CategoryId_seq; Type: SEQUENCE SET; Schema: petstore; Owner: postgres
--

SELECT pg_catalog.setval('petstore."Category_CategoryId_seq"', 25, true);


--
-- TOC entry 2887 (class 0 OID 0)
-- Dependencies: 210
-- Name: Comment_CommentId_seq; Type: SEQUENCE SET; Schema: petstore; Owner: postgres
--

SELECT pg_catalog.setval('petstore."Comment_CommentId_seq"', 20, true);


--
-- TOC entry 2888 (class 0 OID 0)
-- Dependencies: 206
-- Name: OrderItem_OrderItemId_seq; Type: SEQUENCE SET; Schema: petstore; Owner: postgres
--

SELECT pg_catalog.setval('petstore."OrderItem_OrderItemId_seq"', 32, true);


--
-- TOC entry 2889 (class 0 OID 0)
-- Dependencies: 207
-- Name: Order_OrderId_seq; Type: SEQUENCE SET; Schema: petstore; Owner: postgres
--

SELECT pg_catalog.setval('petstore."Order_OrderId_seq"', 29, true);


--
-- TOC entry 2890 (class 0 OID 0)
-- Dependencies: 208
-- Name: Toy_ToyId_seq; Type: SEQUENCE SET; Schema: petstore; Owner: postgres
--

SELECT pg_catalog.setval('petstore."Toy_ToyId_seq"', 55, true);


--
-- TOC entry 2732 (class 2606 OID 16686)
-- Name: Category Category_pkey; Type: CONSTRAINT; Schema: petstore; Owner: postgres
--

ALTER TABLE ONLY petstore."Category"
    ADD CONSTRAINT "Category_pkey" PRIMARY KEY ("CategoryId");


--
-- TOC entry 2734 (class 2606 OID 16800)
-- Name: Comment Comment_pkey; Type: CONSTRAINT; Schema: petstore; Owner: postgres
--

ALTER TABLE ONLY petstore."Comment"
    ADD CONSTRAINT "Comment_pkey" PRIMARY KEY ("CommentId");


--
-- TOC entry 2728 (class 2606 OID 16663)
-- Name: OrderItem OrderItem_pkey; Type: CONSTRAINT; Schema: petstore; Owner: postgres
--

ALTER TABLE ONLY petstore."OrderItem"
    ADD CONSTRAINT "OrderItem_pkey" PRIMARY KEY ("OrderItemId");


--
-- TOC entry 2726 (class 2606 OID 16661)
-- Name: Order Order_pkey; Type: CONSTRAINT; Schema: petstore; Owner: postgres
--

ALTER TABLE ONLY petstore."Order"
    ADD CONSTRAINT "Order_pkey" PRIMARY KEY ("OrderId");


--
-- TOC entry 2723 (class 2606 OID 16675)
-- Name: Toy Toy_pkey; Type: CONSTRAINT; Schema: petstore; Owner: postgres
--

ALTER TABLE ONLY petstore."Toy"
    ADD CONSTRAINT "Toy_pkey" PRIMARY KEY ("ToyId");


--
-- TOC entry 2735 (class 1259 OID 16806)
-- Name: fki_Comment_Toy; Type: INDEX; Schema: petstore; Owner: postgres
--

CREATE INDEX "fki_Comment_Toy" ON petstore."Comment" USING btree ("ToyId");


--
-- TOC entry 2724 (class 1259 OID 16610)
-- Name: fki_OrderItem_Category; Type: INDEX; Schema: petstore; Owner: postgres
--

CREATE INDEX "fki_OrderItem_Category" ON petstore."Toy" USING btree ("CategoryId");


--
-- TOC entry 2729 (class 1259 OID 16588)
-- Name: fki_OrderItem_Order; Type: INDEX; Schema: petstore; Owner: postgres
--

CREATE INDEX "fki_OrderItem_Order" ON petstore."OrderItem" USING btree ("OrderId");


--
-- TOC entry 2730 (class 1259 OID 16594)
-- Name: fki_OrderItem_Toy; Type: INDEX; Schema: petstore; Owner: postgres
--

CREATE INDEX "fki_OrderItem_Toy" ON petstore."OrderItem" USING btree ("ToyId");


--
-- TOC entry 2739 (class 2606 OID 16808)
-- Name: Comment Comment_Toy; Type: FK CONSTRAINT; Schema: petstore; Owner: postgres
--

ALTER TABLE ONLY petstore."Comment"
    ADD CONSTRAINT "Comment_Toy" FOREIGN KEY ("ToyId") REFERENCES petstore."Toy"("ToyId") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2737 (class 2606 OID 16818)
-- Name: OrderItem OrderItem_Order; Type: FK CONSTRAINT; Schema: petstore; Owner: postgres
--

ALTER TABLE ONLY petstore."OrderItem"
    ADD CONSTRAINT "OrderItem_Order" FOREIGN KEY ("OrderId") REFERENCES petstore."Order"("OrderId") ON UPDATE SET NULL ON DELETE SET NULL;


--
-- TOC entry 2738 (class 2606 OID 16758)
-- Name: OrderItem OrderItem_Toy; Type: FK CONSTRAINT; Schema: petstore; Owner: postgres
--

ALTER TABLE ONLY petstore."OrderItem"
    ADD CONSTRAINT "OrderItem_Toy" FOREIGN KEY ("ToyId") REFERENCES petstore."Toy"("ToyId") ON UPDATE SET NULL ON DELETE SET NULL;


--
-- TOC entry 2736 (class 2606 OID 16813)
-- Name: Toy Toy_Category; Type: FK CONSTRAINT; Schema: petstore; Owner: postgres
--

ALTER TABLE ONLY petstore."Toy"
    ADD CONSTRAINT "Toy_Category" FOREIGN KEY ("CategoryId") REFERENCES petstore."Category"("CategoryId") ON UPDATE SET NULL ON DELETE SET NULL;


-- Completed on 2020-01-31 10:16:04

--
-- PostgreSQL database dump complete
--

